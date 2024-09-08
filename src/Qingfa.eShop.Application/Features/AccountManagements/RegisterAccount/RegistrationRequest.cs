using QingFa.EShop.Application.Features.Common.Addresses;

namespace QingFa.EShop.Application.Features.AccountManagements.RegisterAccount
{
    public record RegistrationRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public AddressTransfer? ShippingAddress { get; set; } = AddressTransfer.Empty;
        public AddressTransfer? BillingAddress { get; set; } = AddressTransfer.Empty;
        public string? Username { get; set; } // Optional username property
    }

}
