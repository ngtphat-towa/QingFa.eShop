namespace QingFa.EShop.Application.Features.AccountManagements.ExternalLogin
{
    public class ExternalAccountRequest
    {
        // The unique identifier for the external provider
        public string ProviderKey { get; set; } = default!;

        // The name of the external provider (e.g., "Google", "Facebook")
        public string LoginProvider { get; set; } = default!;

        // The display name of the external provider (e.g., "Google", "Facebook")
        public string ProviderDisplayName { get; set; } = default!;

        // The email address of the user (required if creating a new user)
        public string Email { get; set; } = default!;

        // The username of the user (required if creating a new user)
        public string Username { get; set; } = default!;

        // Optional: The first name of the user (used if creating a new user)
        public string FirstName { get; set; } = default!;

        // Optional: The last name of the user (used if creating a new user)
        public string LastName { get; set; } = default!;

        // Optional: The phone number of the user (used if creating a new user)
        public string PhoneNumber { get; set; } = default!;
    }

}
