namespace QingFa.EShop.Application.Features.AccountManagements.Errors
{
    internal static class ValidationMessages
    {
        // General Validation
        public const string RequiredField = "This field is required.";
        public const string InvalidFormat = "The format is invalid.";

        // Account Registration and Login
        public const string EmailRequired = "Email is required when neither Username nor PhoneNumber is provided.";
        public const string UsernameRequired = "Username is required when neither Email nor PhoneNumber is provided.";
        public const string PhoneNumberRequired = "PhoneNumber is required when neither Email nor Username is provided.";
        public const string PasswordRequired = "Password is required.";
        public const string PasswordMinLength = "Password must be at least 6 characters long.";
        public const string PasswordMismatch = "Passwords do not match.";
        public const string ValidEmailRequired = "A valid email is required.";
        public const string InvalidPhoneNumber = "Phone number is not valid.";

        // User Details
        public const string FirstNameRequired = "First name is required.";
        public const string LastNameRequired = "Last name is required.";
        public const string AddressRequired = "Address is required.";

        // Confirmation and Tokens
        public const string ConfirmCodeRequired = "Confirmation code is required.";
        public const string NewPasswordRequired = "New password is required.";
        public const string OldTokenRequired = "Old token is required.";
        public const string AccessTokenRequired = "Access token is required for third-party login.";
        public const string ProviderRequired = "Provider is required for third-party login.";

        // Username-related
        public const string UserNameRequired = "Username is required.";
    }
}
