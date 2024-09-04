using QingFa.EShop.Application.Features.Common.Addresses;

namespace QingFa.EShop.Application.Features.AccountManagements.UpdateUserInfo
{
    public class UpdateUserInfoRequest
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UserName { get; set; }
        public AddressTransfer? ShippingAddress { get; set; } = AddressTransfer.Empty;
        public AddressTransfer? BillingAddress { get; set; } = AddressTransfer.Empty;
        public string? ProfileImageUrl { get; set; }
    }
}
