namespace QingFa.EShop.Infrastructure.Identity.Settings
{
    public class JwtSettings
    {
        public string Issuer { get; init; } = default!;

        public string Audience { get; init; } = default!;

        public string Secret { get; init; } = default!;

        public int AccessTokenExpirationInMinutes { get; set; } = 15;

        public int RefreshTokenExpirationInDays { get; set; } = 7;
    }
}
