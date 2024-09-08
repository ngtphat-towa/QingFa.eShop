using System.ComponentModel.DataAnnotations.Schema;

using QingFa.EShop.Domain.Core.Entities;
using QingFa.EShop.Domain.Identities.Entities;

namespace QingFa.EShop.Infrastructure.Identity.Entities.Roles
{
    public class RolePermission : AuditEntity
    {
        public RolePermission() : base(Guid.Empty) { }

        public RolePermission(Guid id, Guid roleId, Guid permissionId)
            : base(id)
        {
            RoleId = roleId;
            PermissionId = permissionId;
        }

        [ForeignKey(nameof(Role))]
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; } = default!;

        [ForeignKey(nameof(Permission))]
        public Guid PermissionId { get; set; }
        public virtual Permission Permission { get; set; } = default!;
    }
}
