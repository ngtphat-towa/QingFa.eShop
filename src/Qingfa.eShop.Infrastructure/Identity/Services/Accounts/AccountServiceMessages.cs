namespace QingFa.EShop.Infrastructure.Identity.Services.Accounts
{
    public static class AccountServiceMessages
    {
        // Success Messages
        public const string RegistrationSuccess = "Registration successful. Please check your email to confirm your account.";
        public const string LoginSuccess = "Login successful.";
        public const string TokenRefreshSuccess = "Token refreshed successfully.";
        public const string EmailConfirmed = "Email confirmed successfully.";
        public const string EmailResent = "Confirmation email resent successfully.";
        public const string PasswordResetLinkSent = "Password reset link sent successfully.";
        public const string PasswordResetSuccess = "Password reset successfully.";
        public const string TwoFAEnabled = "Two-factor authentication has been enabled.";
        public const string TwoFADisabled = "Two-factor authentication has been disabled.";
        public const string RecoveryCodesSent = "Recovery codes sent successfully.";
        public const string ExternalLoginSuccess = "External login successful.";
        public const string UserInfoRetrieved = "User information retrieved successfully.";
        public const string UserInfoUpdated = "User information updated successfully.";

        // Error Messages
        public const string UserNotFound = "User not found.";
        public const string InvalidLoginAttempt = "Invalid login attempt.";
        public const string TokenGenerationFailed = "Token generation failed.";
        public const string EmailConfirmationFailed = "Email confirmation failed.";
        public const string EmailResendFailed = "Failed to resend confirmation email.";
        public const string PasswordResetFailed = "Password reset failed.";
        public const string TwoFAOperationFailed = "Operation failed while managing two-factor authentication.";
        public const string RecoveryCodesGenerationFailed = "Failed to generate recovery codes.";
        public const string ExternalLoginFailed = "External login failed.";
        public const string UserUpdateFailed = "User information update failed.";
        public const string InvalidToken = "Invalid token.";
        public const string CredentialNotFound = "Credential not found.";
        public const string Unauthorized = "Unauthorized.";
        public const string ValidationFailure = "Validation failure.";
        public const string UnexpectedError = "An unexpected error occurred.";
    }
}
