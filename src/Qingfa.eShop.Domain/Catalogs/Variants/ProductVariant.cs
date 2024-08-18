using ErrorOr;
using QingFa.EShop.Domain.Catalogs.Products;
using QingFa.EShop.Domain.DomainModels.Core;
using QingFa.EShop.Domain.DomainModels.Errors;
using QingFa.EShop.Domain.Commons.ValueObjects;
using MediatR;

namespace QingFa.EShop.Domain.Catalogs.Variants
{
    public class ProductVariant : Entity<ProductVariantId>
    {
        #region Properties

        public ProductId ProductId { get; private set; }
        public string SKU { get; private set; }
        public Price Price { get; private set; }
        public int StockQuantity { get; private set; }
        public bool IsActive { get; private set; }

        // Navigation property
        public virtual ICollection<ProductVariantAttribute> ProductVariantAttributes { get; private set; } = new HashSet<ProductVariantAttribute>();

        #endregion

        #region Constructors

        // Constructor used by factory methods
        private ProductVariant(
            ProductVariantId id,
            ProductId productId,
            string sku,
            Price price,
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

        // Parameterless constructor for ORM
#pragma warning disable CS8618 
        protected ProductVariant() : base(default!) { }
#pragma warning restore CS8618

        #endregion

        #region Factory Methods

        // Create method with all parameters including Price object
        public static ErrorOr<ProductVariant> CreateWithPrice(
            ProductVariantId id,
            ProductId productId,
            string sku,
            Price price,
            int stockQuantity,
            bool isActive)
        {
            if (productId == null)
                return CoreErrors.ValidationError(nameof(productId), "ProductId cannot be null.");

            if (string.IsNullOrWhiteSpace(sku))
                return CoreErrors.ValidationError(nameof(sku), "SKU cannot be empty.");

            if (price == null)
                return CoreErrors.ValidationError(nameof(price), "Price cannot be null.");

            if (stockQuantity < 0)
                return CoreErrors.ValidationError(nameof(stockQuantity), "StockQuantity cannot be negative.");

            return new ProductVariant(id, productId, sku, price, stockQuantity, isActive);
        }

        // Create method with price provided as decimal and currency
        public static ErrorOr<ProductVariant> CreateWithDefaults(
            ProductVariantId id,
            ProductId productId,
            string sku,
            decimal priceValue = 0.0m, // Default price value
            string currency = "USD",   // Default currency
            int stockQuantity = 0,     // Default stock quantity
            bool isActive = true)      // Default status
        {
            if (productId == null)
                return CoreErrors.ValidationError(nameof(productId), "ProductId cannot be null.");

            if (string.IsNullOrWhiteSpace(sku))
                return CoreErrors.ValidationError(nameof(sku), "SKU cannot be empty.");

            if (priceValue < 0)
                return CoreErrors.ValidationError(nameof(priceValue), "Price cannot be negative.");

            if (stockQuantity < 0)
                return CoreErrors.ValidationError(nameof(stockQuantity), "StockQuantity cannot be negative.");

            var priceResult = Price.Create(priceValue, currency);
            if (priceResult.IsError)
                return priceResult.Errors.First(); // Returning the first error from Price.Create

            return new ProductVariant(id, productId, sku, priceResult.Value, stockQuantity, isActive);
        }

        #endregion

        #region Methods

        public ErrorOr<Unit> UpdateDetails(
            string sku,
            decimal priceValue,
            string currency,
            int stockQuantity,
            bool isActive)
        {
            if (string.IsNullOrWhiteSpace(sku))
                return CoreErrors.ValidationError(nameof(sku), "SKU cannot be empty.");

            if (priceValue < 0)
                return CoreErrors.ValidationError(nameof(priceValue), "Price cannot be negative.");

            if (stockQuantity < 0)
                return CoreErrors.ValidationError(nameof(stockQuantity), "StockQuantity cannot be negative.");

            var priceResult = Price.Create(priceValue, currency);
            if (priceResult.IsError)
                return priceResult.Errors.First();

            SKU = sku;
            Price = priceResult.Value;
            StockQuantity = stockQuantity;
            IsActive = isActive;

            return Unit.Value;
        }

        #endregion

        #region Equality & Hashing

        public override bool Equals(object? obj)
        {
            return obj is ProductVariant other &&
                   Id.Equals(other.Id) &&
                   SKU == other.SKU &&
                   Price.Equals(other.Price) &&
                   StockQuantity == other.StockQuantity &&
                   IsActive == other.IsActive;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, SKU, Price, StockQuantity, IsActive);
        }

        #endregion
    }
}
