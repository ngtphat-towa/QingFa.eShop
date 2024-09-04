using Microsoft.AspNetCore.Identity;

using QingFa.EShop.Domain.Common.ValueObjects;
using QingFa.EShop.Domain.Core.Entities;
using QingFa.EShop.Domain.Core.Enums;
using QingFa.EShop.Infrastructure.Identity.Entities.Roles;

namespace QingFa.EShop.Infrastructure.Identity.Entities
{
    public class AppUser : IdentityUser<Guid>, IAuditable
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Address? ShippingAddress { get; set; } = Address.Empty;
        public Address? BillingAddress { get; set; } = Address.Empty;
        public bool IsVerified => EmailConfirmed || PhoneNumberConfirmed;
        public int FailedLoginAttempts { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetExpires { get; set; }
        public DateTime? LastPasswordChange { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTimeOffset Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
        public EntityStatus Status { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
        public virtual ICollection<Role> Roles { get; set; } = new HashSet<Role>();

        public void UpdateAuditInfo(string? lastModifiedBy = null)
        {
            LastModified = DateTimeOffset.UtcNow;
            LastModifiedBy = lastModifiedBy;
        }

        public void SetStatus(EntityStatus? status = default, string? lastModifiedBy = null)
        {
            Status = status ?? Status;
            UpdateAuditInfo(lastModifiedBy);
        }

        public bool IsActive() => Status == EntityStatus.Active;
    }
}
