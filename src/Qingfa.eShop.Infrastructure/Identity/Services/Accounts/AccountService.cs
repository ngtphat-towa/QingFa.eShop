using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.AccountManagements.ConfirmEmail;
using QingFa.EShop.Application.Features.AccountManagements.Enable2FA;
using QingFa.EShop.Application.Features.AccountManagements.ExternalLogin;
using QingFa.EShop.Application.Features.AccountManagements.RegisterAccount;
using QingFa.EShop.Application.Features.AccountManagements.RefreshToken;
using QingFa.EShop.Application.Features.AccountManagements.UpdateUserInfo;
using QingFa.EShop.Application.Features.AccountManagements.ForgotPassword;
using QingFa.EShop.Application.Features.AccountManagements.ResetPassword;
using QingFa.EShop.Application.Features.AccountManagements.LogInAccount;
using QingFa.EShop.Application.Features.AccountManagements.ResendConfirmationEmail;
using QingFa.EShop.Application.Features.AccountManagements.Models;
using QingFa.EShop.Infrastructure.Identity.Services.RefreshTokens;
using QingFa.EShop.Infrastructure.Identity.Services.Tokens;
using QingFa.EShop.Infrastructure.Identity.Settings;
using QingFa.EShop.Domain.Common.ValueObjects;
using QingFa.EShop.Domain.Core.Enums;
using Microsoft.EntityFrameworkCore;
using QingFa.EShop.Application.Features.Common.Addresses;
using QingFa.EShop.Application.Features.AccountManagements.Services;
using QingFa.EShop.Application.Features.AccountManagements.ChangePassword;
using QingFa.EShop.Application.Features.AccountManagements.UpdateEmail;
using System.ComponentModel.DataAnnotations;
using QingFa.EShop.Infrastructure.Identity.Entities.Roles;
using QingFa.EShop.Infrastructure.Identity.Services.Emails;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using QingFa.EShop.Domain.Identities.Entities;

namespace QingFa.EShop.Infrastructure.Identity.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private static readonly EmailAddressAttribute _emailAddressAttribute = new();
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<AccountService> _logger;
        private readonly JwtSettings _jwtSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RoleManager<Role> _roleManager;
        private readonly IDistributedCache _cache;

        public AccountService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IRefreshTokenService refreshTokenService,
            IEmailSender emailSender,
            ILogger<AccountService> logger,
            IOptions<JwtSettings> options,
            IHttpContextAccessor httpContextAccessor,
            RoleManager<Role> roleManager,
            IDistributedCache cache)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _refreshTokenService = refreshTokenService;
            _emailSender = emailSender;
            _logger = logger;
            _jwtSettings = options.Value;
            _httpContextAccessor = httpContextAccessor;
            _roleManager = roleManager;
            _cache = cache;
        }

        // User Registration
        public async Task<Result<RegisterResponse>> RegisterAsync(RegistrationRequest request)
        {
            // Validate request
            if (request == null)
            {
                return Result<RegisterResponse>.InvalidArgument(nameof(request), "Registration request cannot be null.");
            }

            // Validate password
            if (string.IsNullOrEmpty(request.Password))
            {
                return Result<RegisterResponse>.ValidationFailure("Password is required.");
            }

            if (request.Password != request.ConfirmPassword)
            {
                return Result<RegisterResponse>.ValidationFailure("Passwords do not match.");
            }

            // Validate email
            if (string.IsNullOrEmpty(request.Email) || !_emailAddressAttribute.IsValid(request.Email))
            {
                return Result<RegisterResponse>.ValidationFailure("Invalid email address.");
            }

            // Generate username
            string username = GenerateUsername(request);

            // Create user
            var user = new AppUser
            {
                UserName = username,
                Email = request.Email,
                FirstName = request.FirstName ?? string.Empty,
                LastName = request.LastName ?? string.Empty,
                PhoneNumber = request.PhoneNumber,
                ShippingAddress = CreateAddress(request.ShippingAddress),
                BillingAddress = CreateAddress(request.BillingAddress),
                Created = DateTimeOffset.UtcNow,
                Status = EntityStatus.Active
            };

            // Create the user
            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                return Result<RegisterResponse>.ValidationFailure(createResult.Errors.Select(e => e.Description));
            }

            // Check if role exists
            const string roleToAssign = "User"; // or "Customer" based on your requirements
            if (!await _roleManager.RoleExistsAsync(roleToAssign))
            {
                return Result<RegisterResponse>.InvalidOperation("Role assignment", $"Role '{roleToAssign}' does not exist.");
            }

            // Assign role
            var roleResult = await _userManager.AddToRoleAsync(user, roleToAssign);
            if (!roleResult.Succeeded)
            {
                return Result<RegisterResponse>.Failure(roleResult.Errors.Select(e => e.Description));
            }

            // Generate email confirmation token
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = GenerateConfirmationLink(user.Id, token);

            // Send confirmation email
            try
            {
                await _emailSender.SendEmailAsync(user.Email!, "Confirm your email",
                    $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send confirmation email for user {UserId}", user.Id);
                return Result<RegisterResponse>.Failure("Registration successful, but failed to send confirmation email. Please contact support.");
            }

            return Result<RegisterResponse>.Success(new RegisterResponse(user.Id, user.UserName!, user.Email!,
                "Registration successful. Please check your email to confirm your account."));
        }

        // User Login with Account Lockout
        public async Task<Result<TokenResponse>> LoginAsync(LogInAccountRequest request)
        {
            var user = await FindUserAsync(request);
            if (user == null)
            {
                _logger.LogWarning("Login attempt failed: User not found for {Email} or {UserName} or {PhoneNumber}",
                    request.Email, request.UserName, request.PhoneNumber);
                return Result<TokenResponse>.NotFound("User", "Invalid login credentials.");
            }

            if (!user.EmailConfirmed)
            {
                return Result<TokenResponse>.PreconditionFailed("Email not confirmed. Please check your email for the confirmation link.");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName!, request.Password,
                request.RememberMe, lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                _logger.LogWarning("User {UserId} account is locked out.", user.Id);
                return Result<TokenResponse>.RateLimited("Account locked due to multiple failed login attempts. Please try again later.");
            }

            if (result.RequiresTwoFactor)
            {
                // Handle two-factor authentication
                return Result<TokenResponse>.InvalidOperation("TwoFactorRequired", "Two-factor authentication is required.");
            }

            if (!result.Succeeded)
            {
                _logger.LogWarning("Failed login attempt for user {UserId}", user.Id);
                return Result<TokenResponse>.Failure("Invalid login credentials.");
            }

            _logger.LogInformation("User {UserId} logged in successfully", user.Id);

            var token = await _tokenService.GenerateTokenAsync(user);
            var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user);

            return Result<TokenResponse>.Success(new TokenResponse
            {
                TokenType = "Bearer",
                AccessToken = token,
                ExpiresIn = _jwtSettings.AccessTokenExpirationInMinutes * 60, // Convert minutes to seconds
                RefreshToken = refreshToken
            });
        }

        // Refresh Token
        public async Task<Result<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
            {
                return Result<TokenResponse>.ValidationFailure("Refresh token is required.");
            }

            try
            {
                // Extract the principal and claims from the provided refresh token
                var principal = _tokenService.GetPrincipalFromToken(request.RefreshToken);
                var refreshTokenGuid = principal?.FindFirstValue(ClaimTypes.NameIdentifier);
                var userIdClaim = principal?.FindFirstValue(ClaimTypes.Name);

                if (string.IsNullOrEmpty(refreshTokenGuid) || string.IsNullOrEmpty(userIdClaim))
                {
                    return Result<TokenResponse>.NotFound("Refresh token", AccountServiceMessages.InvalidToken);
                }

                var userId = Guid.Parse(userIdClaim);

                // Find the user associated with the refresh token
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return Result<TokenResponse>.NotFound("User", AccountServiceMessages.UserNotFound);
                }

                // Validate the provided refresh token
                if (!await _refreshTokenService.ValidateRefreshTokenAsync(user, request.RefreshToken))
                {
                    await _refreshTokenService.RevokeRefreshTokenAsync(request.RefreshToken);
                    return Result<TokenResponse>.InvalidOperation("Refresh token", AccountServiceMessages.InvalidToken);
                }

                // Generate a new access token
                var newAccessToken = await _tokenService.GenerateTokenAsync(user);

                // Rotate the refresh token
                var newRefreshToken = await _refreshTokenService.RotateRefreshTokenAsync(user, request.RefreshToken);

                if (newRefreshToken == null)
                {
                    return Result<TokenResponse>.UnexpectedError(new Exception("Failed to generate new refresh token."));
                }

                _logger.LogInformation("Refresh token rotated successfully for user {UserId}", user.Id);

                return Result<TokenResponse>.Success(new TokenResponse
                {
                    TokenType = "Bearer",
                    AccessToken = newAccessToken,
                    ExpiresIn = _jwtSettings.AccessTokenExpirationInMinutes * 60,
                    RefreshToken = newRefreshToken
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while refreshing the token");
                return Result<TokenResponse>.UnexpectedError(ex);
            }
        }

        // Confirm Email
        public async Task<Result<string>> ConfirmEmailAsync(ConfirmEmailRequest request)
        {
            _logger.LogDebug("Confirming email for user {UserId} with token {Token}", request.UserId, request.Token);

            if (string.IsNullOrEmpty(request.UserId) || string.IsNullOrEmpty(request.Token))
            {
                return Result<string>.ValidationFailure("User ID and token are required.");
            }

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                _logger.LogWarning("Attempt to confirm email for non-existent user with ID: {UserId}", request.UserId);
                return Result<string>.NotFound("User", "User not found.");
            }

            _logger.LogDebug("User found: {UserId}, Email: {Email}, EmailConfirmed: {EmailConfirmed}",
                user.Id, user.Email, user.EmailConfirmed);

            if (user.EmailConfirmed)
            {
                return Result<string>.Success("Email is already confirmed.");
            }

            try
            {
                IdentityResult result;
                if (string.IsNullOrEmpty(request.ChangeEmail))
                {
                    _logger.LogDebug("Calling ConfirmEmailAsync for user {UserId}", user.Id);
                    result = await _userManager.ConfirmEmailAsync(user, request.Token);
                }
                else
                {
                    _logger.LogDebug("Calling UpdateEmailAndUsernameAsync for user {UserId}", user.Id);
                    result = await UpdateEmailAndUsernameAsync(user, request);
                }

                if (result.Succeeded)
                {
                    _logger.LogInformation("Email confirmed successfully for user {UserId}", user.Id);
                    return Result<string>.Success("Email confirmed successfully. You can now log in with your account.");
                }
                else
                {
                    _logger.LogWarning("Failed to confirm email for user {UserId}. Errors: {Errors}",
                        user.Id, string.Join(", ", result.Errors.Select(e => e.Description)));
                    return Result<string>.Failure(result.Errors.Select(e => e.Description).ToArray());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while confirming email for user {UserId}", user.Id);
                return Result<string>.UnexpectedError(ex);
            }
        }

        private async Task<IdentityResult> UpdateEmailAndUsernameAsync(AppUser user, ConfirmEmailRequest request)
        {
            var result = await _userManager.ChangeEmailAsync(user, request.ChangeEmail!, request.Token);
            if (!result.Succeeded) return result;

            result = await _userManager.SetUserNameAsync(user, request.ChangeEmail);
            return result;
        }

        // Resend Confirmation Email
        public async Task<Result<string>> ResendConfirmationEmailAsync(ResendConfirmationEmailRequest request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return Result<string>.ValidationFailure("Email is required.");
            }

            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    _logger.LogWarning("Attempt to resend confirmation email for non-existent user with email: {Email}", request.Email);
                    return Result<string>.NotFound("User", AccountServiceMessages.UserNotFound);
                }

                if (user.EmailConfirmed)
                {
                    return Result<string>.Success("Email is already confirmed.");
                }

                // Check for rate limiting (e.g., no more than 3 requests per hour)
                if (await IsRateLimitExceededAsync(user.Id))
                {
                    return Result<string>.RateLimited("Too many requests. Please try again later.");
                }

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = GenerateConfirmationLink(user.Id, token);

                await _emailSender.SendEmailAsync(user.Email!, "Confirm your email",
                    $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");

                _logger.LogInformation("Confirmation email resent successfully for user {UserId}", user.Id);
                return Result<string>.Success(AccountServiceMessages.EmailResent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while resending confirmation email for email: {Email}", request.Email);
                return Result<string>.UnexpectedError(ex);
            }
        }

        private async Task<bool> IsRateLimitExceededAsync(Guid userId)
        {
            var cacheKey = $"email_confirmation_rate_limit_{userId}";
            var currentCount = await _cache.GetStringAsync(cacheKey);

            if (int.TryParse(currentCount, out int count))
            {
                if (count >= 3) // Rate limit exceeded
                {
                    return true;
                }
                await _cache.SetStringAsync(cacheKey, (count + 1).ToString(), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
                });
            }
            else
            {
                await _cache.SetStringAsync(cacheKey, "1", new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
                });
            }

            return false;
        }

        // Forgot Password
        public async Task<Result<string>> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return Result<string>.ValidationFailure("Email is required.");
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                _logger.LogWarning("Password reset requested for non-existent user with email: {Email}", request.Email);
                return Result<string>.Success(AccountServiceMessages.PasswordResetLinkSent);
            }

            if (!user.EmailConfirmed)
            {
                return Result<string>.Failure("Email not confirmed. Please confirm your email before resetting your password.");
            }

            try
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetLink = GenerateResetLink(user.Email!, token);

                await _emailSender.SendEmailAsync(user.Email!, "Reset your password",
                    $"Please reset your password by <a href='{resetLink}'>clicking here</a>. If you didn't request this, please ignore this email.");

                _logger.LogInformation("Password reset link sent to user {UserId}", user.Id);
                return Result<string>.Success(AccountServiceMessages.PasswordResetLinkSent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing password reset for user {UserId}", user.Id);
                return Result<string>.UnexpectedError("An error occurred while processing your request. Please try again later.");
            }
        }

        // Reset Password
        public async Task<Result<string>> ResetPasswordAsync(ResetPasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Token) || string.IsNullOrWhiteSpace(request.NewPassword))
            {
                return Result<string>.ValidationFailure("Email, token, and new password are required.");
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                _logger.LogWarning("Password reset attempted for non-existent user with email: {Email}", request.Email);
                return Result<string>.Failure("Invalid password reset request.");
            }

            try
            {
                var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
                if (!result.Succeeded)
                {
                    _logger.LogWarning("Password reset failed for user {UserId}. Errors: {Errors}",
                        user.Id, string.Join(", ", result.Errors.Select(e => e.Description)));
                    return Result<string>.Failure(result.Errors.Select(e => e.Description).ToArray());
                }

                // Optionally, you might want to invalidate all existing refresh tokens for this user
                await _refreshTokenService.RevokeAllRefreshTokensForUserAsync(user.Id);

                _logger.LogInformation("Password reset successful for user {UserId}", user.Id);
                return Result<string>.Success(AccountServiceMessages.PasswordResetSuccess);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while resetting password for user {UserId}", user.Id);
                return Result<string>.UnexpectedError("An error occurred while resetting your password. Please try again later.");
            }
        }

        // Manage 2FA
        public async Task<Result<TwoFactorResponse>> Manage2FAAsync(TwoFactorRequest request)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext?.User == null)
            {
                return Result<TwoFactorResponse>.Unauthorized();
            }

            var user = await _userManager.GetUserAsync(httpContext.User);
            if (user == null)
            {
                return Result<TwoFactorResponse>.NotFound(AccountServiceMessages.UserNotFound);
            }

            if (request.Enable == true)
            {
                if (request.ResetSharedKey)
                {
                    return Result<TwoFactorResponse>.ValidationFailure("CannotResetSharedKeyAndEnable",
                        "Resetting the 2fa shared key must disable 2fa until a 2fa token based on the new shared key is validated.");
                }
                else if (string.IsNullOrEmpty(request.TwoFactorCode))
                {
                    return Result<TwoFactorResponse>.ValidationFailure("RequiresTwoFactor",
                        "No 2fa token was provided by the request. A valid 2fa token is required to enable 2fa.");
                }
                else if (!await _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider, request.TwoFactorCode))
                {
                    return Result<TwoFactorResponse>.ValidationFailure("InvalidTwoFactorCode",
                        "The 2fa token provided by the request was invalid. A valid 2fa token is required to enable 2fa.");
                }

                await _userManager.SetTwoFactorEnabledAsync(user, true);
            }
            else if (request.Enable == false || request.ResetSharedKey)
            {
                await _userManager.SetTwoFactorEnabledAsync(user, false);
            }

            if (request.ResetSharedKey)
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
            }

            string[]? recoveryCodes = null;
            if (request.ResetRecoveryCodes || (request.Enable == true && await _userManager.CountRecoveryCodesAsync(user) == 0))
            {
                var recoveryCodesEnumerable = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
                recoveryCodes = recoveryCodesEnumerable?.ToArray();
            }

            if (request.ForgetMachine)
            {
                await _signInManager.ForgetTwoFactorClientAsync();
            }

            var key = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(key))
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                key = await _userManager.GetAuthenticatorKeyAsync(user);

                if (string.IsNullOrEmpty(key))
                {
                    return Result<TwoFactorResponse>.UnexpectedError(new NotSupportedException("The user manager must produce an authenticator key after reset."));
                }
            }

            return Result<TwoFactorResponse>.Success(new TwoFactorResponse
            {
                SharedKey = key,
                RecoveryCodes = recoveryCodes,
                RecoveryCodesLeft = recoveryCodes?.Length ?? await _userManager.CountRecoveryCodesAsync(user),
                IsTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user),
                IsMachineRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user),
            });
        }

        // External Login
        public async Task<Result<ExternalLoginResponse>> ExternalLoginAsync(ExternalLoginInfo info)
        {
            if (info == null)
            {
                return Result<ExternalLoginResponse>.ValidationFailure("External login information is required.");
            }

            try
            {
                // Check if the external login is already associated with a user
                var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                if (user == null)
                {
                    // If not, try to create a new user or link to an existing one
                    user = await CreateOrLinkExternalUserAsync(info);
                    if (user == null)
                    {
                        return Result<ExternalLoginResponse>.Failure("Failed to create or link external user.");
                    }
                }

                // Sign in the user
                await _signInManager.SignInAsync(user, isPersistent: false);

                // Generate tokens
                var token = await _tokenService.GenerateTokenAsync(user);
                var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user);

                _logger.LogInformation("External login successful for user {UserId}", user.Id);
                return Result<ExternalLoginResponse>.Success(new ExternalLoginResponse
                {
                    JwtToken = token,
                    RefreshToken = refreshToken,
                    UserId = user.Id.ToString()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during external login");
                return Result<ExternalLoginResponse>.UnexpectedError("An unexpected error occurred. Please try again later.");
            }
        }

        private async Task<AppUser?> CreateOrLinkExternalUserAsync(ExternalLoginInfo info)
        {
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
            {
                _logger.LogWarning("External login provider did not provide an email for the user");
                return null;
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                // Link the external login to the existing user
                var result = await _userManager.AddLoginAsync(user, info);
                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to add external login for existing user. Errors: {Errors}",
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                    return null;
                }
            }
            else
            {
                // Create a new user
                user = new AppUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName) ?? string.Empty,
                    LastName = info.Principal.FindFirstValue(ClaimTypes.Surname) ?? string.Empty,
                    EmailConfirmed = true,
                    Created = DateTimeOffset.UtcNow,
                    Status = EntityStatus.Active
                };

                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to create new user for external login. Errors: {Errors}",
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                    return null;
                }

                result = await _userManager.AddLoginAsync(user, info);
                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to add external login for new user. Errors: {Errors}",
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                    await _userManager.DeleteAsync(user);
                    return null;
                }

                // Assign default role
                await _userManager.AddToRoleAsync(user, "User");
            }

            return user;
        }

        public async Task<Result<string>> LinkExternalAccountAsync(ClaimsPrincipal currentUser, ExternalLoginInfo info)
        {
            if (info == null)
            {
                return Result<string>.ValidationFailure("External login information is required.");
            }

            try
            {
                var user = await _userManager.GetUserAsync(currentUser);
                if (user == null)
                {
                    return Result<string>.NotFound("User not found.");
                }

                var result = await _userManager.AddLoginAsync(user, info);
                if (!result.Succeeded)
                {
                    return Result<string>.Failure(result.Errors.Select(e => e.Description).ToArray());
                }

                _logger.LogInformation("External account linked successfully for user {UserId}", user.Id);
                return Result<string>.Success("External account linked successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while linking external account");
                return Result<string>.UnexpectedError("An unexpected error occurred. Please try again later.");
            }
        }

        // Get User Info
        public async Task<Result<UserInfoResponse>> GetUserInfoAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext?.User == null)
            {
                return Result<UserInfoResponse>.Unauthorized();
            }

            var user = await _userManager.GetUserAsync(httpContext.User);
            if (user == null)
            {
                return Result<UserInfoResponse>.NotFound("User");
            }

            return Result<UserInfoResponse>.Success(new UserInfoResponse
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber,
                IsVerified = user.EmailConfirmed,
                ShippingAddress = MapAddress(user.ShippingAddress),
                BillingAddress = MapAddress(user.BillingAddress),
                ProfileImageUrl = user.ProfileImageUrl,
                TwoFactorEnabled = user.TwoFactorEnabled
            });
        }

        // Update User Info
        public async Task<Result<string>> UpdateUserInfoAsync(UpdateUserInfoRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                return Result<string>.NotFound("User");
            }

            user.FirstName = request.FirstName ?? user.FirstName;
            user.LastName = request.LastName ?? user.LastName;
            user.Email = request.Email ?? user.Email;
            user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
            user.ShippingAddress = request.ShippingAddress != null ? CreateAddress(request.ShippingAddress) : user.ShippingAddress;
            user.BillingAddress = request.BillingAddress != null ? CreateAddress(request.BillingAddress) : user.BillingAddress;
            user.ProfileImageUrl = request.ProfileImageUrl ?? user.ProfileImageUrl;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return Result<string>.Failure(result.Errors.Select(e => e.Description).ToArray());
            }

            const string roleToAssign = "User"; // or "Customer"
            var currentRoles = await _userManager.GetRolesAsync(user);

            if (currentRoles.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                {
                    return Result<string>.Failure(removeResult.Errors.Select(e => e.Description).ToArray());
                }
            }

            var addResult = await _userManager.AddToRolesAsync(user, new List<string> { roleToAssign });
            if (!addResult.Succeeded)
            {
                return Result<string>.Failure(addResult.Errors.Select(e => e.Description).ToArray());
            }

            return Result<string>.Success(AccountServiceMessages.UserInfoUpdated);
        }

        // Change Password
        public async Task<Result<string>> ChangePasswordAsync(ChangePasswordRequest request)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext?.User == null)
            {
                return Result<string>.Unauthorized();
            }

            var user = await _userManager.GetUserAsync(httpContext.User);
            if (user == null)
            {
                return Result<string>.NotFound("User");
            }

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                return Result<string>.Failure(result.Errors.Select(e => e.Description).ToArray());
            }

            return Result<string>.Success("Password changed successfully.");
        }

        // Update Email
        public async Task<Result<string>> UpdateEmailAsync(UpdateEmailRequest request)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext?.User == null)
            {
                return Result<string>.Unauthorized();
            }

            var user = await _userManager.GetUserAsync(httpContext.User);
            if (user == null)
            {
                return Result<string>.NotFound("User");
            }

            user.Email = request.NewEmail;

            var token = await _userManager.GenerateChangeEmailTokenAsync(user, request.NewEmail);
            var confirmationLink = GenerateChangeEmailLink(user.Id, request.NewEmail, token);

            await _emailSender.SendEmailAsync(request.NewEmail, "Confirm your email address change",
                $"Please confirm your email change by <a href='{confirmationLink}'>clicking here</a>.");

            return Result<string>.Success("Email change requested. Please check your new email to confirm.");
        }

        // Delete Account
        public async Task<Result<string>> DeleteAccountAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext?.User == null)
            {
                return Result<string>.Unauthorized();
            }

            var user = await _userManager.GetUserAsync(httpContext.User);
            if (user == null)
            {
                return Result<string>.NotFound("User");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return Result<string>.Failure(result.Errors.Select(e => e.Description).ToArray());
            }

            return Result<string>.Success("Account deleted successfully.");
        }

        // Helper Methods
        private string GenerateUsername(RegistrationRequest request)
        {
            if (!string.IsNullOrEmpty(request.Username))
                return request.Username;
            if (!string.IsNullOrEmpty(request.PhoneNumber))
                return request.PhoneNumber;
            if (!string.IsNullOrEmpty(request.Email))
                return request.Email.Split('@')[0];
            return $"{request.FirstName}.{request.LastName}".ToLower();
        }

        private static Address? CreateAddress(AddressTransfer? addressRequest)
        {
            return addressRequest != null ? Address.Create(
                addressRequest.Street,
                addressRequest.City,
                addressRequest.State,
                addressRequest.Country,
                addressRequest.PostalCode
            ) : null;
        }

        private static AddressTransfer MapAddress(Address? address)
        {
            return address != null ? new AddressTransfer
            {
                Street = address.Street,
                City = address.City,
                State = address.State,
                PostalCode = address.PostalCode
            } : AddressTransfer.Empty;
        }

        private static string GenerateConfirmationLink(Guid userId, string token)
        {
            return $"https://yourapp.com/confirmemail?userId={userId}&token={WebUtility.UrlEncode(token)}";
        }

        private static string GenerateResetLink(string email, string token)
        {
            return $"https://yourapp.com/resetpassword?email={email}&token={WebUtility.UrlEncode(token)}";
        }

        private static string GenerateChangeEmailLink(Guid userId, string newEmail, string token)
        {
            return $"https://yourapp.com/changeemail?userId={userId}&newEmail={newEmail}&token={WebUtility.UrlEncode(token)}";
        }

        private async Task<AppUser?> FindUserAsync(LogInAccountRequest request)
        {
            if (!string.IsNullOrEmpty(request.Email))
                return await _userManager.FindByEmailAsync(request.Email);

            if (!string.IsNullOrEmpty(request.UserName))
                return await _userManager.FindByNameAsync(request.UserName);

            if (!string.IsNullOrEmpty(request.PhoneNumber))
                return await _userManager.Users
                    .FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);

            return null;
        }

        public async Task<Result<string>> EnsureExternalAccountAsync(ExternalAccountRequest request)
        {
            if (string.IsNullOrEmpty(request.ProviderKey) || string.IsNullOrEmpty(request.LoginProvider))
                return Result<string>.ValidationFailure("ProviderKey and LoginProvider are required.");

            try
            {
                var user = await _userManager.FindByLoginAsync(request.LoginProvider, request.ProviderKey);
                if (user != null)
                {
                    _logger.LogInformation("External login already linked to user {UserId}", user.Id);
                    return Result<string>.Success("External login is already linked to an existing account.");
                }

                user = await _userManager.FindByEmailAsync(request.Email)
                    ?? (!string.IsNullOrEmpty(request.Username) ? await _userManager.FindByNameAsync(request.Username) : null);

                if (user == null)
                {
                    user = new AppUser
                    {
                        UserName = request.Username ?? request.Email?.Split('@')[0],
                        Email = request.Email,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        PhoneNumber = request.PhoneNumber,
                        Created = DateTimeOffset.UtcNow,
                        Status = EntityStatus.Active,
                        EmailConfirmed = true
                    };

                    var result = await _userManager.CreateAsync(user);
                    if (!result.Succeeded)
                    {
                        _logger.LogWarning("Failed to create user for external login. Errors: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                        return Result<string>.Failure(result.Errors.Select(e => e.Description).ToArray());
                    }

                    await _userManager.AddToRoleAsync(user, "User");
                }

                var loginInfo = new UserLoginInfo(request.LoginProvider, request.ProviderKey, request.ProviderDisplayName);
                var loginResult = await _userManager.AddLoginAsync(user, loginInfo);
                if (!loginResult.Succeeded)
                {
                    _logger.LogWarning("Failed to add external login for user {UserId}. Errors: {Errors}", user.Id, string.Join(", ", loginResult.Errors.Select(e => e.Description)));
                    return Result<string>.Failure(loginResult.Errors.Select(e => e.Description).ToArray());
                }

                await UpdateExternalUserInfo(user, request);

                _logger.LogInformation("External login successfully ensured for user {UserId}", user.Id);
                return Result<string>.Success(AccountServiceMessages.ExternalLoginSuccess);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while ensuring external account");
                return Result<string>.UnexpectedError("An unexpected error occurred. Please try again later.");
            }
        }
        private async Task UpdateExternalUserInfo(AppUser user, ExternalAccountRequest request)
        {
            bool needsUpdate = false;

            if (!string.IsNullOrEmpty(request.FirstName) && user.FirstName != request.FirstName)
            {
                user.FirstName = request.FirstName;
                needsUpdate = true;
            }

            if (!string.IsNullOrEmpty(request.LastName) && user.LastName != request.LastName)
            {
                user.LastName = request.LastName;
                needsUpdate = true;
            }

            if (!string.IsNullOrEmpty(request.Email) && user.Email != request.Email)
            {
                user.Email = request.Email;
                needsUpdate = true;
            }

            if (needsUpdate)
            {
                await _userManager.UpdateAsync(user);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, request.ProviderKey),
                new Claim(ClaimTypes.AuthenticationMethod, request.LoginProvider)
            };

            await _userManager.AddClaimsAsync(user, claims);
        }

        public Task<Result<string>> InitiateExternalLoginAsync(string provider, string returnUrl)
        {
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, returnUrl);
            return Task.FromResult(Result<string>.Success(properties.RedirectUri!));
        }

        public async Task<Result<ExternalLoginResponse>> HandleExternalLoginCallbackAsync(string returnUrl, string remoteError)
        {
            if (!string.IsNullOrEmpty(remoteError))
            {
                return Result<ExternalLoginResponse>.Failure($"Error from external provider: {remoteError}");
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return Result<ExternalLoginResponse>.Failure("Error loading external login information.");
            }

            return await ExternalLoginAsync(info);
        }
    }
}
