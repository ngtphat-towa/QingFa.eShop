using System.Collections.Generic;

using ErrorOr;

using QingFa.EShop.Domain.Catalogs.Products;
using QingFa.EShop.Domain.DomainModels.Core;
using QingFa.EShop.Domain.DomainModels.Errors;

namespace QingFa.EShop.Domain.Catalogs.Variants
{
    public class ProductVariant : Entity<ProductVariantId>
    {
        #region Properties

        public ProductId ProductId { get; private set; }
        public string SKU { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }
        public bool IsActive { get; private set; }

        // Navigation property
        public virtual ICollection<ProductVariantAttribute> ProductVariantAttributes { get; private set; } = new HashSet<ProductVariantAttribute>();

        #endregion

        #region Constructors

        protected ProductVariant(
            ProductVariantId id,
            ProductId productId,
            string sku,
            decimal price,
            int stockQuantity,
            bool isActive
        ) : base(id)
        {
            ProductId = productId;
            SKU = sku;
            Price = price;
            StockQuantity = stockQuantity;
            IsActive = isActive;
        }

#pragma warning disable CS8618
        protected ProductVariant() : base(default!) { }
#pragma warning restore CS8618

        #endregion

        #region Factory Methods

        public static ErrorOr<ProductVariant> Create(
            ProductVariantId id,
            ProductId productId,
            string sku,
            decimal price,
            int stockQuantity,
            bool isActive
        )
        {
            if (string.IsNullOrWhiteSpace(sku))
                return CoreErrors.ValidationError(nameof(sku), "SKU cannot be empty.");

            if (price < 0)
                return CoreErrors.ValidationError(nameof(price), "Price cannot be negative.");

            if (stockQuantity < 0)
                return CoreErrors.ValidationError(nameof(stockQuantity), "StockQuantity cannot be negative.");

            var productVariant = new ProductVariant(
                id,
                productId,
                sku,
                price,
                stockQuantity,
                isActive
            );

            return productVariant;
        }

        #endregion

        #region Methods

        public void UpdateDetails(
            string sku,
            decimal price,
            int stockQuantity,
            bool isActive
        )
        {
            if (string.IsNullOrWhiteSpace(sku))
                throw new ArgumentException("SKU cannot be empty.", nameof(sku));

            if (price < 0)
                throw new ArgumentException("Price cannot be negative.", nameof(price));

            if (stockQuantity < 0)
                throw new ArgumentException("StockQuantity cannot be negative.", nameof(stockQuantity));

            SKU = sku;
            Price = price;
            StockQuantity = stockQuantity;
            IsActive = isActive;
        }

        #endregion
    }
}
