namespace QingFa.EShop.Application.Features.AccountManagements.LogInAccount
{
    public record LogInAccountRequest
    {
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}
