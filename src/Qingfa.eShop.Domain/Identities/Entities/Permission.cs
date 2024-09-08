using QingFa.EShop.Domain.Catalogs.Enums;
using QingFa.EShop.Domain.Common.Enums;
using QingFa.EShop.Domain.Core.Entities;

namespace QingFa.EShop.Domain.Identities.Entities
{
    public class Permission : AuditEntity
    {

        public Permission(
            Guid id,
            string name,
            PermissionAction permissionAction,
            ResourceScope resourceScope,
            string? description = null,
            DateTimeOffset created = default,
            string? createdBy = null,
            DateTimeOffset lastModified = default,
            string? lastModifiedBy = null)
            : base(id)
        {
            Name = name;
            PermissionAction = permissionAction;
            ResourceScope = resourceScope;
            Description = description;
            Created = created;
            CreatedBy = createdBy;
            LastModified = lastModified;
            LastModifiedBy = lastModifiedBy;

            // Format the policy string
            Policy = $"{permissionAction}:{resourceScope}";
        }

        // Properties
        public string Name { get; set; } = string.Empty;

        public PermissionAction PermissionAction { get; set; }

        public ResourceScope ResourceScope { get; set; }

        public string? Description { get; set; }
        public string Policy { get; set; }

        public string FormattedPolicy => $"{ResourceScope}:{PermissionAction}";

        // Static factory method
        public static Permission Create(
            Guid id,
            string name,
            PermissionAction permissionAction,
            ResourceScope resourceScope,
            string? description = null,
            DateTimeOffset created = default,
            string? createdBy = null,
            DateTimeOffset lastModified = default,
            string? lastModifiedBy = null)
        {
            var permission = new Permission(
                id,
                name,
                permissionAction,
                resourceScope,
                description,
                created,
                createdBy,
                lastModified,
                lastModifiedBy);
            return permission;
        }
    }
}
