using QingFa.EShop.Domain.Core.Entities;
using QingFa.EShop.Domain.Core.Exceptions;

namespace QingFa.EShop.Domain.Catalogs.Entities.Attributes
{
    public class ProductAttributeOption : AuditEntity
    {
        public string OptionValue { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public bool IsDefault { get; private set; }
        public int SortOrder { get; private set; }
        public Guid ProductAttributeId { get; private set; }

        public virtual ProductAttribute Attribute { get; private set; } = default!;

        private ProductAttributeOption(Guid id, string optionValue, string description, bool isDefault, int sortOrder, Guid attributeId)
            : base(id)
        {
            OptionValue = !string.IsNullOrWhiteSpace(optionValue) ? optionValue : throw CoreException.NullOrEmptyArgument(nameof(optionValue));
            Description = description;
            IsDefault = isDefault;
            SortOrder = sortOrder;
            ProductAttributeId = attributeId;
        }

        private ProductAttributeOption() : base(default!)
        {
        }

        public static ProductAttributeOption Create(string optionValue, string description, bool isDefault, int sortOrder, Guid attributeId)
        {
            return new ProductAttributeOption(Guid.NewGuid(), optionValue, description, isDefault, sortOrder, attributeId);
        }

        public void Update(string optionValue, string description, bool isDefault, int sortOrder, string? lastModifiedBy = null)
        {
            if (string.IsNullOrWhiteSpace(optionValue)) throw CoreException.NullOrEmptyArgument(nameof(optionValue));

            OptionValue = optionValue;
            Description = description;
            IsDefault = isDefault;
            SortOrder = sortOrder;
            UpdateAuditInfo(lastModifiedBy);
        }
    }
}
