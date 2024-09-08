using System.Security.Claims;

using QingFa.EShop.Domain.Identities.Entities;

namespace QingFa.EShop.Infrastructure.Identity.Services.Tokens
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(AppUser user);
        ClaimsPrincipal GetPrincipalFromToken(string token);
    }
}
