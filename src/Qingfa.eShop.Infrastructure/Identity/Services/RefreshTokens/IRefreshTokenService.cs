using System;
using System.Threading.Tasks;

using QingFa.EShop.Domain.Identities.Entities;

namespace QingFa.EShop.Infrastructure.Identity.Services.RefreshTokens
{
    public interface IRefreshTokenService
    {
        /// <summary>
        /// Generates a new refresh token for a specified user.
        /// </summary>
        /// <param name="user">The user for whom the refresh token is being generated.</param>
        /// <returns>The generated refresh token as a string.</returns>
        Task<string> GenerateRefreshTokenAsync(AppUser user);

        /// <summary>
        /// Validates a refresh token for a specified user.
        /// </summary>
        /// <param name="user">The user whose refresh token is being validated.</param>
        /// <param name="refreshToken">The refresh token to validate.</param>
        /// <returns>True if the token is valid; otherwise, false.</returns>
        Task<bool> ValidateRefreshTokenAsync(AppUser user, string refreshToken);

        /// <summary>
        /// Revokes a specific refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token to revoke.</param>
        /// <returns>True if the token was successfully revoked; otherwise, false.</returns>
        Task<bool> RevokeRefreshTokenAsync(string refreshToken);

        /// <summary>
        /// Revokes all refresh tokens for a specified user.
        /// </summary>
        /// <param name="userId">The ID of the user whose tokens are to be revoked.</param>
        /// <returns>True if all tokens were successfully revoked; otherwise, false.</returns>
        Task<bool> RevokeAllRefreshTokensForUserAsync(Guid userId);

        /// <summary>
        /// Rotates an existing refresh token by revoking the old one and generating a new one.
        /// </summary>
        /// <param name="user">The user for whom the refresh token is being rotated.</param>
        /// <param name="oldRefreshToken">The old refresh token to be revoked.</param>
        /// <returns>The new refresh token, or null if rotation failed.</returns>
        Task<string?> RotateRefreshTokenAsync(AppUser user, string oldRefreshToken);
    }
}
