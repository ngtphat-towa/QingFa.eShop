using QingFa.EShop.Domain.Common.Enums;
using QingFa.EShop.Domain.Core.Entities;

namespace QingFa.EShop.Infrastructure.Identity.Entities.Permissions
{
    public class Permission : AuditEntity
    {
        public Permission(
            Guid id,
            string name,
            PermissionAction action,
            ResourceScope resource,
            string? description = null)
            : base(id)
        {
            Name = name;
            Action = action;
            Resource = resource;
            Description = description;
        }

        public string Name { get; set; } = string.Empty;
        public PermissionAction Action { get; set; }
        public ResourceScope Resource { get; set; }
        public string? Description { get; set; }
    }
}
