using ErrorOr;

using QingFa.EShop.Domain.Catalogs.Attributes;
using QingFa.EShop.Domain.Catalogs.Products;
using QingFa.EShop.Domain.DomainModels.Core;
using QingFa.EShop.Domain.DomainModels.Errors;

namespace QingFa.EShop.Domain.Catalogs.Variants
{
    public class ProductVariantAttribute : Entity<ProductVariantAttributeId>
    {
        #region Properties

        public ProductVariantId ProductVariantId { get; private set; }
        public ProductAttributeId AttributeId { get; private set; }
        public AttributeOptionId? AttributeOptionId { get; private set; }
        public string? CustomValue { get; private set; }
        public bool IsRequired { get; private set; }
        public bool IsVisibleToCustomer { get; private set; }

        // Navigation properties
        public virtual ProductVariant? ProductVariant { get; set; }
        public virtual ProductAttribute? Attribute { get; set; }
        public virtual AttributeOption? AttributeOption { get; set; }

        #endregion

        #region Constructors

        protected ProductVariantAttribute(
            ProductVariantAttributeId id,
            ProductVariantId productVariantId,
            ProductAttributeId attributeId,
            AttributeOptionId? attributeOptionId,
            string? customValue,
            bool isRequired,
            bool isVisibleToCustomer
        ) : base(id)
        {
            ProductVariantId = productVariantId;
            AttributeId = attributeId;
            AttributeOptionId = attributeOptionId;
            CustomValue = customValue;
            IsRequired = isRequired;
            IsVisibleToCustomer = isVisibleToCustomer;
        }

#pragma warning disable CS8618
        protected ProductVariantAttribute() : base(default!) { }
#pragma warning restore CS8618

        #endregion

        #region Factory Methods

        public static ErrorOr<ProductVariantAttribute> Create(
            ProductVariantAttributeId id,
            ProductVariantId productVariantId,
            ProductAttributeId attributeId,
            AttributeOptionId? attributeOptionId,
            string? customValue,
            bool isRequired,
            bool isVisibleToCustomer
        )
        {
            if (productVariantId == null)
                return CoreErrors.ValidationError(nameof(productVariantId), "ProductVariantId cannot be null.");

            if (attributeId == null)
                return CoreErrors.ValidationError(nameof(attributeId), "AttributeId cannot be null.");

            if (attributeOptionId == null && string.IsNullOrEmpty(customValue))
                return CoreErrors.ValidationError(nameof(attributeOptionId), "Either AttributeOptionId or CustomValue must be provided.");

            var productVariantAttribute = new ProductVariantAttribute(
                id,
                productVariantId,
                attributeId,
                attributeOptionId,
                customValue,
                isRequired,
                isVisibleToCustomer
            );

            return productVariantAttribute;
        }

        #endregion

        #region Methods

        public void UpdateDetails(
            ProductAttributeId attributeId,
            AttributeOptionId? attributeOptionId,
            string? customValue
        )
        {
            if (attributeId == null)
                throw new ArgumentException("AttributeId cannot be null.", nameof(attributeId));

            if (attributeOptionId == null && string.IsNullOrEmpty(customValue))
                throw new ArgumentException("Either AttributeOptionId or CustomValue must be provided.", nameof(attributeOptionId));

            AttributeId = attributeId;
            AttributeOptionId = attributeOptionId;
            CustomValue = customValue;
        }

        #endregion
    }
}
