using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs
{
    public class Inventory : Entity<InventoryId>
    {
        public CatalogProductId CatalogProductId { get; private set; }
        public virtual CatalogProduct? CatalogProduct { get; private set; }
        public int AvailableStock { get; private set; }
        public int RestockThreshold { get; private set; }
        public int MaxStockThreshold { get; private set; }
        public bool OnReorder { get; private set; }

        private Inventory(InventoryId inventoryId, CatalogProductId catalogProductId, int availableStock, int restockThreshold, int maxStockThreshold, bool onReorder)
            : base(inventoryId)
        {
            if (catalogProductId == null) throw CoreException.NullArgument(nameof(catalogProductId));
            if (availableStock < 0) throw CoreException.InvalidArgument(nameof(availableStock));
            if (restockThreshold < 0) throw CoreException.InvalidArgument(nameof(restockThreshold));
            if (maxStockThreshold < restockThreshold) throw CoreException.InvalidArgument(nameof(maxStockThreshold));

            CatalogProductId = catalogProductId;
            AvailableStock = availableStock;
            RestockThreshold = restockThreshold;
            MaxStockThreshold = maxStockThreshold;
            OnReorder = onReorder;
        }

        public static Inventory Create(InventoryId inventoryId, CatalogProductId catalogProductId, int availableStock, int restockThreshold, int maxStockThreshold, bool onReorder)
        {
            return new Inventory(inventoryId, catalogProductId, availableStock, restockThreshold, maxStockThreshold, onReorder);
        }

        public void AddStock(int quantity)
        {
            if (quantity < 0) throw CoreException.InvalidArgument(nameof(quantity));
            AvailableStock += quantity;
            CheckReorderStatus();
            Updated = DateTime.UtcNow;
        }

        public void RemoveStock(int quantity)
        {
            if (quantity < 0) throw CoreException.InvalidArgument(nameof(quantity));
            if (AvailableStock < quantity) throw CoreException.InvalidArgument(nameof(AvailableStock));
            AvailableStock -= quantity;
            CheckReorderStatus();
            Updated = DateTime.UtcNow;
        }

        private void CheckReorderStatus()
        {
            OnReorder = AvailableStock <= RestockThreshold;
        }
#pragma warning disable CS8618
        protected Inventory()
#pragma warning restore CS8618
        {

        }
    }
}
