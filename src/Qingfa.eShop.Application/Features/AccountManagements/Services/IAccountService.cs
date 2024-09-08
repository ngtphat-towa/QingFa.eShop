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
using QingFa.EShop.Application.Features.AccountManagements.ChangePassword;
using QingFa.EShop.Application.Features.AccountManagements.UpdateEmail;
using QingFa.EShop.Application.Features.AccountManagements.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace QingFa.EShop.Application.Features.AccountManagements.Services
{
    public interface IAccountService
    {
        // Registration and Authentication
        Task<Result<RegisterResponse>> RegisterAsync(RegistrationRequest request);
        Task<Result<TokenResponse>> LoginAsync(LogInAccountRequest request);
        Task<Result<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
        Task<Result<string>> ConfirmEmailAsync(ConfirmEmailRequest request);
        Task<Result<string>> ResendConfirmationEmailAsync(ResendConfirmationEmailRequest request);
        Task<Result<string>> ForgotPasswordAsync(ForgotPasswordRequest request);
        Task<Result<string>> ResetPasswordAsync(ResetPasswordRequest request);
        Task<Result<TwoFactorResponse>> Manage2FAAsync(TwoFactorRequest request);
        Task<Result<ExternalLoginResponse>> ExternalLoginAsync(ExternalLoginInfo info);
        Task<Result<UserInfoResponse>> GetUserInfoAsync();
        Task<Result<string>> UpdateUserInfoAsync(UpdateUserInfoRequest request);
        Task<Result<string>> ChangePasswordAsync(ChangePasswordRequest request);
        Task<Result<string>> UpdateEmailAsync(UpdateEmailRequest request);
        Task<Result<string>> DeleteAccountAsync();
        Task<Result<string>> EnsureExternalAccountAsync(ExternalAccountRequest request);
        Task<Result<string>> LinkExternalAccountAsync(ClaimsPrincipal currentUser, ExternalLoginInfo info);
        Task<Result<string>> InitiateExternalLoginAsync(string provider, string returnUrl);
        Task<Result<ExternalLoginResponse>> HandleExternalLoginCallbackAsync(string returnUrl, string remoteError);
    }
}
