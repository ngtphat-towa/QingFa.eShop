using QingFa.EShop.Domain.Catalogs.Enums;
using QingFa.EShop.Domain.Common.ValueObjects;
using QingFa.EShop.Domain.Core.Entities;

namespace QingFa.EShop.Domain.Catalogs.Entities.Attributes
{
    public class ProductVariant : AuditEntity
    {
        public Guid ProductId { get; private set; }
        public string SKU { get; private set; } = string.Empty;
        public Money Price { get; private set; } = default!;
        public int StockLevel { get; private set; }
        public VariantStatus VariantStatus { get; private set; } = default!;

        // Navigation property
        public virtual Product Product { get; private set; } = default!;
        public virtual ICollection<ProductVariantAttribute> VariantAttributes { get; private set; } = new HashSet<ProductVariantAttribute>();

        private ProductVariant(Guid id, Guid productId, string sku, Money price, int stockLevel, VariantStatus status)
            : base(id)
        {
            ProductId = productId;
            SKU = sku;
            Price = price;
            StockLevel = stockLevel;
            VariantStatus = status;
        }

        private ProductVariant() : base(default!)
        {
        }

        public static ProductVariant Create(Guid productId, string sku, Money price, int stockLevel, VariantStatus status)
        {
            return new ProductVariant(Guid.NewGuid(), productId, sku, price, stockLevel, status);
        }

        public void UpdateStockLevel(int stockLevel, string? lastModifiedBy = null)
        {
            StockLevel = stockLevel;
            UpdateAuditInfo(lastModifiedBy);
        }

        public void UpdatePrice(Money price, string? lastModifiedBy = null)
        {
            Price = price;
            UpdateAuditInfo(lastModifiedBy);
        }


    }
}
