using QingFa.EShop.Application.Features.Common.Addresses;

namespace QingFa.EShop.Application.Features.AccountManagements.UpdateUserInfo
{
    public record UserInfoResponse
    {
        public Guid UserId { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string? PhoneNumber { get; init; }
        public bool IsVerified { get; init; }
        public AddressTransfer ShippingAddress { get; init; } = AddressTransfer.Empty;
        public AddressTransfer BillingAddress { get; init; } = AddressTransfer.Empty;
        public string? ProfileImageUrl { get; init; }
        public bool TwoFactorEnabled { get; init; }
    }
}
