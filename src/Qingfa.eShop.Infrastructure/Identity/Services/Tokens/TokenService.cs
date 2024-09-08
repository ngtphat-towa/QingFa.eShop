using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QingFa.EShop.Domain.Core.Exceptions;
using QingFa.EShop.Infrastructure.Identity.Settings;
using Microsoft.Extensions.Logging;
using QingFa.EShop.Domain.Identities.Entities;

namespace QingFa.EShop.Infrastructure.Identity.Services.Tokens
{
    internal class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IOptions<JwtSettings> jwtSettings, ILogger<TokenService> logger)
        {
            _jwtSettings = jwtSettings.Value;
            _logger = logger;
        }

        public async Task<string> GenerateTokenAsync(AppUser user)
        {
            if (user.Id == Guid.Empty) throw CoreException.NullArgument(nameof(user.Id));
            if (user.Email == null) throw CoreException.NullArgument(nameof(user.Email));
            if (user.UserName == null) throw CoreException.NullArgument(nameof(user.UserName));

            try
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    _jwtSettings.Issuer,
                    _jwtSettings.Audience,
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationInMinutes),
                    signingCredentials: creds
                );

                return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating token for user {UserId}", user.Id);
                throw; 
            }
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = key
            };

            try
            {
                var principal = handler.ValidateToken(token, parameters, out var securityToken);
                return principal;
            }
            catch (SecurityTokenExpiredException ex)
            {
                _logger.LogWarning(ex, "Token expired");
                throw; 
            }
            catch (SecurityTokenInvalidSignatureException ex)
            {
                _logger.LogWarning(ex, "Invalid token signature");
                throw; 
            }
            catch (SecurityTokenInvalidIssuerException ex)
            {
                _logger.LogWarning(ex, "Invalid token issuer");
                throw; 
            }
        }
    }
}
