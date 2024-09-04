using QingFa.EShop.Infrastructure.Identity.Entities;

namespace QingFa.EShop.Infrastructure.Identity.Services.RefreshTokens
{
    public interface IRefreshTokenService
    {
        Task<string> GenerateRefreshTokenAsync(AppUser user);
        Task<bool> RevokeAllRefreshTokensForUserAsync(Guid userId);
        Task<bool> RevokeRefreshTokenAsync(string refreshToken);
        Task<string?> RotateRefreshTokenAsync(AppUser user, string oldRefreshToken);
        Task<bool> ValidateRefreshTokenAsync(AppUser user, string refreshToken);
    }
}