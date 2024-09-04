using System.Security.Claims;

using QingFa.EShop.Infrastructure.Identity.Entities;

namespace QingFa.EShop.Infrastructure.Identity.Services.Tokens
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(AppUser user);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
