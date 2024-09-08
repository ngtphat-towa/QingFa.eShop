using Microsoft.AspNetCore.Mvc;
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
using QingFa.EShop.Application.Features.AccountManagements.ChangePassword;
using QingFa.EShop.Application.Features.AccountManagements.UpdateEmail;
using Swashbuckle.AspNetCore.Annotations;
using QingFa.EShop.Api.Models;
using QingFa.EShop.Application.Features.AccountManagements.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace QingFa.EShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        [SwaggerOperation(Summary = "Registers a new user account.")]
        [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            var result = await _accountService.RegisterAsync(request);
            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(Register), new { id = result.Value?.UserId }, result.Value);
            }
            return HandleResult(result);
        }

        [HttpPost("login")]
        [SwaggerOperation(Summary = "Logs in a user and returns a token.")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LogInAccountRequest request)
        {
            var result = await _accountService.LoginAsync(request);
            return HandleResult(result);
        }

        [HttpPost("refresh-token")]
        [SwaggerOperation(Summary = "Refreshes an authentication token.")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var result = await _accountService.RefreshTokenAsync(request);
            return HandleResult(result);
        }

        [HttpPost("confirm-email")]
        [SwaggerOperation(Summary = "Confirms a user's email address.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
        {
            var result = await _accountService.ConfirmEmailAsync(request);
            return HandleResult(result);
        }

        [HttpPost("resend-confirmation-email")]
        [SwaggerOperation(Summary = "Resends the email confirmation link.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResendConfirmationEmail([FromBody] ResendConfirmationEmailRequest request)
        {
            var result = await _accountService.ResendConfirmationEmailAsync(request);
            return HandleResult(result);
        }

        [HttpPost("forgot-password")]
        [SwaggerOperation(Summary = "Sends a password reset link to the user.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var result = await _accountService.ForgotPasswordAsync(request);
            return HandleResult(result);
        }

        [HttpPost("reset-password")]
        [SwaggerOperation(Summary = "Resets the user's password.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var result = await _accountService.ResetPasswordAsync(request);
            return HandleResult(result);
        }

        [HttpPost("external-login")]
        [SwaggerOperation(Summary = "Logs in the user using an external provider.")]
        [ProducesResponseType(typeof(ExternalLoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalLoginInfo request)
        {
            var result = await _accountService.ExternalLoginAsync(request);
            return HandleResult(result);
        }

        [Authorize]
        [HttpGet("user-info")]
        [SwaggerOperation(Summary = "Retrieves information about the currently authenticated user.")]
        [ProducesResponseType(typeof(UserInfoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUserInfo()
        {
            var result = await _accountService.GetUserInfoAsync();
            return HandleResult(result);
        }

        [Authorize]
        [HttpPut("update-user-info")]
        [SwaggerOperation(Summary = "Updates the information for the currently authenticated user.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserInfoRequest request)
        {
            var result = await _accountService.UpdateUserInfoAsync(request);
            return HandleResult(result);
        }

        [Authorize]
        [HttpPost("change-password")]
        [SwaggerOperation(Summary = "Changes the user's password.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var result = await _accountService.ChangePasswordAsync(request);
            return HandleResult(result);
        }

        [Authorize]
        [HttpPost("update-email")]
        [SwaggerOperation(Summary = "Updates the user's email address.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateEmail([FromBody] UpdateEmailRequest request)
        {
            var result = await _accountService.UpdateEmailAsync(request);
            return HandleResult(result);
        }

        [Authorize]
        [HttpDelete("delete-account")]
        [SwaggerOperation(Summary = "Deletes the user's account.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAccount()
        {
            var result = await _accountService.DeleteAccountAsync();
            return HandleResult(result);
        }

        [HttpPost("ensure-external-account")]
        [SwaggerOperation(Summary = "Ensures the external account is linked or created.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EnsureExternalAccount([FromBody] ExternalAccountRequest request)
        {
            var result = await _accountService.EnsureExternalAccountAsync(request);
            return HandleResult(result);
        }

        [Authorize]
        [HttpPost("manage-2fa")]
        [SwaggerOperation(Summary = "Manages two-factor authentication settings for the user.")]
        [ProducesResponseType(typeof(TwoFactorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Manage2FA([FromBody] TwoFactorRequest request)
        {
            var result = await _accountService.Manage2FAAsync(request);
            return HandleResult(result);
        }

        [HttpPost("link-external-account")]
        [Authorize]
        [SwaggerOperation(Summary = "Links an external account to the current user.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LinkExternalAccount([FromBody] ExternalLoginInfo info)
        {
            var result = await _accountService.LinkExternalAccountAsync(User, info);
            return HandleResult(result);
        }

        [HttpGet("initiate-external-login")]
        [SwaggerOperation(Summary = "Initiates the external login process.")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InitiateExternalLogin([FromQuery] string provider, [FromQuery] string returnUrl)
        {
            var result = await _accountService.InitiateExternalLoginAsync(provider, returnUrl);
            return HandleResult(result);
        }

        [HttpGet("handle-external-login-callback")]
        [SwaggerOperation(Summary = "Handles the callback from external login providers.")]
        [ProducesResponseType(typeof(ExternalLoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> HandleExternalLoginCallback([FromQuery] string returnUrl, [FromQuery] string remoteError)
        {
            var result = await _accountService.HandleExternalLoginCallbackAsync(returnUrl, remoteError);
            return HandleResult(result);
        }
    }
}
