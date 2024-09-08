namespace QingFa.EShop.Application.Features.AccountManagements.RefreshToken
{
    public record RefreshTokenRequest
    {
        public string RefreshToken { get; init; } = default!;
    }
}
