using QingFa.EShop.Domain.Core.Entities;
using QingFa.EShop.Domain.Core.Exceptions;

namespace QingFa.EShop.Domain.Catalogs.Entities.Attributes
{
    public class ProductVariantAttribute : AuditEntity
    {
        public Guid ProductVariantId { get; private set; }
        public Guid ProductAttributeId { get; private set; }
        public Guid ProductAttributeOptionId { get; private set; }
        public string Value { get; private set; } = string.Empty;
        public bool IsRequired { get; private set; }
        public bool IsVisibleToCustomer { get; private set; }

        // Navigation properties
        public virtual ProductVariant Variant { get; private set; } = default!;
        public virtual ProductAttribute Attribute { get; private set; } = default!;
        public virtual ProductAttributeOption Option { get; private set; } = default!;

        private ProductVariantAttribute(Guid id, Guid variantId, Guid attributeId, Guid optionId, string value, bool isRequired, bool isVisibleToCustomer)
            : base(id)
        {
            ProductVariantId = variantId;
            ProductAttributeId = attributeId;
            ProductAttributeOptionId = optionId;
            Value = !string.IsNullOrWhiteSpace(value) ? value : throw CoreException.NullOrEmptyArgument(nameof(value));
            IsRequired = isRequired;
            IsVisibleToCustomer = isVisibleToCustomer;
        }

        private ProductVariantAttribute() : base(default!)
        {
        }

        public static ProductVariantAttribute Create(Guid variantId, Guid attributeId, Guid optionId, string value, bool isRequired, bool isVisibleToCustomer)
        {
            return new ProductVariantAttribute(Guid.NewGuid(), variantId, attributeId, optionId, value, isRequired, isVisibleToCustomer);
        }

        public void Update(string value, bool isRequired, bool isVisibleToCustomer, string? lastModifiedBy = null)
        {
            if (string.IsNullOrWhiteSpace(value)) throw CoreException.NullOrEmptyArgument(nameof(value));

            Value = value;
            IsRequired = isRequired;
            IsVisibleToCustomer = isVisibleToCustomer;
            UpdateAuditInfo(lastModifiedBy);
        }
    }
}
