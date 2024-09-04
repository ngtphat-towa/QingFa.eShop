namespace QingFa.EShop.Application.Features.AccountManagements.Get2FAStatus
{
    public record Enable2FAStatusResponse
    {
        public bool Is2FAEnabled { get; set; } = default!;
    }
}
