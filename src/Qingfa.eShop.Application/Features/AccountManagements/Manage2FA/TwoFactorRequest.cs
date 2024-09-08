namespace QingFa.EShop.Application.Features.AccountManagements.Enable2FA
{
    public record TwoFactorRequest
    {
        public bool? Enable { get; set; }
        public bool ResetSharedKey { get; set; }
        public string? TwoFactorCode { get; set; }
        public bool ResetRecoveryCodes { get; set; }
        public bool ForgetMachine { get; set; }
    }
}
