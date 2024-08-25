namespace QingFa.EShop.Infrastructure.Authentication.Settings
{
    public class JwtOptions
    {
        public string Issuer { get; init; } = default!;

        public string Audience { get; init; } = default!;

        public string SecretKey { get; init; } = default!;

        public int AccessTokenExpirationInMinutes { get; set; } = 15;

        public int RefreshTokenExpirationInDays { get; set; } = 7;
    }
}
