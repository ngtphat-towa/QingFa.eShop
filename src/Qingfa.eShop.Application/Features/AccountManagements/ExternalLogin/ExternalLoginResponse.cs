namespace QingFa.EShop.Application.Features.AccountManagements.ExternalLogin
{
    public class ExternalLoginResponse
    {
        public string JwtToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty; 
        public string UserId { get; set; } = string.Empty;
    }
}
