namespace QingFa.EShop.Application.Features.Common.Addresses
{
    public record AddressTransfer
    {
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public static AddressTransfer Empty => new();
    }
}
