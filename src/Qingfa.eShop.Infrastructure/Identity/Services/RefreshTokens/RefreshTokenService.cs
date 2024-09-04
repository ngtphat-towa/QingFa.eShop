using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using QingFa.EShop.Domain.Core.Repositories;
using QingFa.EShop.Infrastructure.Identity.Entities;
using QingFa.EShop.Infrastructure.Identity.Settings;
using QingFa.EShop.Infrastructure.Repositories.Identities.RefreshTokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QingFa.EShop.Infrastructure.Identity.Services.RefreshTokens
{
    internal class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<RefreshTokenService> _logger;

        public RefreshTokenService(
            IRefreshTokenRepository refreshTokenRepository,
            IUnitOfWork unitOfWork,
            IOptions<JwtSettings> jwtSettings,
            ILogger<RefreshTokenService> logger)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;
            _jwtSettings = jwtSettings.Value;
            _logger = logger;
        }

        private string GenerateJwtToken(string refreshTokenGuid, DateTime expiresAt)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, refreshTokenGuid)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: expiresAt,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateRefreshTokenAsync(AppUser user)
        {
            try
            {
                var refreshTokenGuid = Guid.NewGuid().ToString();
                var expiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationInDays);

                var jwtToken = GenerateJwtToken(refreshTokenGuid, expiresAt);

                await _refreshTokenRepository.AddAsync(new RefreshToken
                {
                    UserId = user.Id,
                    Token = refreshTokenGuid,
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = expiresAt
                });
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Generated new refresh token for user {UserId}", user.Id);
                return jwtToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating refresh token for user {UserId}", user.Id);
                throw;
            }
        }

        public async Task<bool> ValidateRefreshTokenAsync(AppUser user, string jwtToken)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwtToken);

                var refreshTokenGuid = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(refreshTokenGuid))
                {
                    _logger.LogWarning("Invalid JWT token for user {UserId}", user.Id);
                    return false;
                }

                var storedToken = await _refreshTokenRepository.GetAsync(rt => rt.Token == refreshTokenGuid && rt.UserId == user.Id);

                if (storedToken == null || storedToken.RevokedAt != null || storedToken.ExpiresAt <= DateTime.UtcNow)
                {
                    _logger.LogWarning("Invalid or expired refresh token for user {UserId}", user.Id);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating refresh token for user {UserId}", user.Id);
                throw;
            }
        }

        public async Task<bool> RevokeRefreshTokenAsync(string jwtToken)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwtToken);

                var refreshTokenGuid = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(refreshTokenGuid))
                {
                    _logger.LogWarning("Attempted to revoke invalid JWT token");
                    return false;
                }

                var tokenEntity = await _refreshTokenRepository.GetAsync(rt => rt.Token == refreshTokenGuid);
                if (tokenEntity == null || tokenEntity.RevokedAt != null)
                {
                    _logger.LogWarning("Attempted to revoke invalid or already revoked token");
                    return false;
                }

                tokenEntity.RevokedAt = DateTime.UtcNow;
                await _refreshTokenRepository.UpdateAsync(tokenEntity);
                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("Revoked refresh token {Token}", refreshTokenGuid);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error revoking refresh token {Token}", jwtToken);
                throw;
            }
        }

        public async Task<bool> RevokeAllRefreshTokensForUserAsync(Guid userId)
        {
            try
            {
                var tokens = await _refreshTokenRepository.GetAllAsync(rt => rt.UserId == userId && rt.RevokedAt == null);
                foreach (var token in tokens)
                {
                    token.RevokedAt = DateTime.UtcNow;
                    await _refreshTokenRepository.UpdateAsync(token);
                }
                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("Revoked all refresh tokens for user {UserId}", userId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error revoking all refresh tokens for user {UserId}", userId);
                throw;
            }
        }

        public async Task<string?> RotateRefreshTokenAsync(AppUser user, string jwtToken)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwtToken);

                var refreshTokenGuid = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(refreshTokenGuid))
                {
                    _logger.LogWarning("Attempted to rotate invalid JWT token for user {UserId}", user.Id);
                    return null;
                }

                var storedToken = await _refreshTokenRepository.GetAsync(rt => rt.Token == refreshTokenGuid && rt.UserId == user.Id);
                if (storedToken == null || storedToken.RevokedAt != null || storedToken.ExpiresAt <= DateTime.UtcNow)
                {
                    _logger.LogWarning("Attempted to rotate invalid or expired token for user {UserId}", user.Id);
                    return null;
                }

                var newRefreshTokenGuid = Guid.NewGuid().ToString();
                var newExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationInDays);
                var newJwtToken = GenerateJwtToken(newRefreshTokenGuid, newExpiresAt);

                storedToken.RevokedAt = DateTime.UtcNow;
                await _refreshTokenRepository.UpdateAsync(storedToken);
                await _refreshTokenRepository.AddAsync(new RefreshToken
                {
                    UserId = user.Id,
                    Token = newRefreshTokenGuid,
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = newExpiresAt
                });
                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("Rotated refresh token for user {UserId}", user.Id);
                return newJwtToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error rotating refresh token for user {UserId}", user.Id);
                throw;
            }
        }
    }
}