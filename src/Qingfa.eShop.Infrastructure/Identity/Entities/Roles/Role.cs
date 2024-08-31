using Microsoft.AspNetCore.Identity;

using QingFa.EShop.Domain.Core.Entities;
using QingFa.EShop.Domain.Core.Enums;

namespace QingFa.EShop.Infrastructure.Identity.Entities.Roles
{
    public class Role : IdentityRole<Guid>, IAuditable
    {
        public DateTimeOffset Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
        public string Description { get; set; } = string.Empty;
        public EntityStatus Status { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new HashSet<RolePermission>();
        public virtual ICollection<AppUser> Users { get; set; } = new HashSet<AppUser>();
    }
}
