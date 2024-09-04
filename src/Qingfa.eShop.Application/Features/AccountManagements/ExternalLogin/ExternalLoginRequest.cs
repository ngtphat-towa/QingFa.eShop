namespace QingFa.EShop.Application.Features.AccountManagements.ExternalLogin
{
    public record ExternalLoginRequest
    {
        /// <summary>
        /// The name of the external login provider (e.g., "Google", "Facebook").
        /// </summary>
        public string LoginProvider { get; init; } = string.Empty;

        /// <summary>
        /// The unique key associated with the external login provider.
        /// </summary>
        public string ProviderKey { get; init; } = string.Empty;

        /// <summary>
        /// The email address associated with the external login (optional).
        /// </summary>
        public string? Email { get; init; }

        /// <summary>
        /// Additional properties or tokens returned by the provider (optional).
        /// </summary>
        public IDictionary<string, string>? AdditionalParameters { get; init; }
    }
}
