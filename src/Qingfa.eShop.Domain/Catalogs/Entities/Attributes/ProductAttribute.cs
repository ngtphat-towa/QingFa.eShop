using QingFa.EShop.Domain.Core.Entities;
using QingFa.EShop.Domain.Core.Exceptions;

namespace QingFa.EShop.Domain.Catalogs.Entities.Attributes
{
    public class ProductAttribute : AuditEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string AttributeCode { get; private set; } = string.Empty;
        public AttributeType Type { get; private set; }
        public bool IsRequired { get; private set; }
        public bool IsFilterable { get; private set; }
        public bool ShowToCustomers { get; private set; }
        public int SortOrder { get; private set; }
        public Guid AttributeGroupId { get; private set; }

        // Navigation properties
        public virtual ProductAttributeGroup AttributeGroup { get; private set; } = default!;
        public virtual ICollection<ProductAttributeOption> AttributeOptions { get; private set; } = new HashSet<ProductAttributeOption>();
        public virtual ICollection<ProductVariantAttribute> VariantAttributes { get; private set; } = new HashSet<ProductVariantAttribute>();

        private ProductAttribute(Guid id, string name, string attributeCode, AttributeType type, bool isRequired, bool isFilterable, bool showToCustomers, int sortOrder, Guid groupId)
            : base(id)
        {
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw CoreException.NullOrEmptyArgument(nameof(name));
            AttributeCode = !string.IsNullOrWhiteSpace(attributeCode) ? attributeCode : throw CoreException.NullOrEmptyArgument(nameof(attributeCode));
            Type = type;
            IsRequired = isRequired;
            IsFilterable = isFilterable;
            ShowToCustomers = showToCustomers;
            SortOrder = sortOrder;
            AttributeGroupId = groupId;
        }

        private ProductAttribute() : base(default!)
        {
        }

        public static ProductAttribute Create(string name, string attributeCode, AttributeType type, bool isRequired, bool isFilterable, bool showToCustomers, int sortOrder, Guid groupId)
        {
            return new ProductAttribute(Guid.NewGuid(), name, attributeCode, type, isRequired, isFilterable, showToCustomers, sortOrder, groupId);
        }

        public void Update(string name, string attributeCode, AttributeType type, bool isRequired, bool isFilterable, bool showToCustomers, int sortOrder, string? lastModifiedBy = null)
        {
            if (string.IsNullOrWhiteSpace(name)) throw CoreException.NullOrEmptyArgument(nameof(name));
            if (string.IsNullOrWhiteSpace(attributeCode)) throw CoreException.NullOrEmptyArgument(nameof(attributeCode));

            Name = name;
            AttributeCode = attributeCode;
            Type = type;
            IsRequired = isRequired;
            IsFilterable = isFilterable;
            ShowToCustomers = showToCustomers;
            SortOrder = sortOrder;
            UpdateAuditInfo(lastModifiedBy);
        }

        public enum AttributeType
        {
            Text,
            Number,
            Date,
            Boolean
        }
    }
}
