namespace QingFa.EShop.Application.Features.AccountManagements.Models
{
    public record TokenResponse
    {
        public string TokenType { get; init; } = default!;
        public string AccessToken { get; init; } = default!;
        public int ExpiresIn { get; init; }
        public string RefreshToken { get; init; } = default!;
    }
}
