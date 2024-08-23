using QingFa.EShop.Domain.Core.Entities;
using QingFa.EShop.Domain.Core.Exceptions;

namespace QingFa.EShop.Domain.Catalogs.Entities.Attributes
{
    public class ProductAttributeGroup : AuditEntity
    {
        public string Name { get; private set; } = string.Empty;

        // Navigation property
        public virtual ICollection<ProductAttribute> Attributes { get; private set; } = new HashSet<ProductAttribute>();

        private ProductAttributeGroup(Guid id, string groupName)
            : base(id)
        {
            Name = !string.IsNullOrWhiteSpace(groupName) ? groupName : throw CoreException.NullOrEmptyArgument(nameof(groupName));
        }

        private ProductAttributeGroup() : base(default!)
        {
        }

        public static ProductAttributeGroup Create(string groupName)
        {
            return new ProductAttributeGroup(Guid.NewGuid(), groupName);
        }

        public void UpdateGroupName(string groupName, string? lastModifiedBy = null)
        {
            if (string.IsNullOrWhiteSpace(groupName)) throw CoreException.NullOrEmptyArgument(nameof(groupName));
            Name = groupName;
            UpdateAuditInfo(lastModifiedBy);
        }
    }
}
