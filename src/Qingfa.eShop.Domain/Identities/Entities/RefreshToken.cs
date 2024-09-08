using QingFa.EShop.Domain.Core.Entities;
using QingFa.EShop.Domain.Identities.Entities;

namespace QingFa.EShop.Infrastructure.Identity.Entities
{
    public class RefreshToken : BaseEntity<Guid>
    {
        /// <summary>
        /// Gets or sets the ID of the user associated with this refresh token.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the user associated with this refresh token.
        /// </summary>
        public virtual AppUser User { get; set; } = default!;

        /// <summary>
        /// Gets or sets the token string.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the expiration date and time of the token.
        /// </summary>
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// Gets or sets the creation date and time of the token.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the token was revoked.
        /// Null if the token is not revoked.
        /// </summary>
        public DateTime? RevokedAt { get; set; }
    }
}
