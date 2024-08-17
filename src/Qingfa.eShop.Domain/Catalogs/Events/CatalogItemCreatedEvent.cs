using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.Common.Enums;
using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.Events
{
    public class CatalogItemCreatedEvent : Event
    {
        public CatalogItemCreatedEvent(ProductId id)
        {
            // Initialization logic
            // TOOD: Implement the logic and handler after this
        }
    }

    public class CatalogItemUpdatedEvent : Event
    {
        public CatalogItemUpdatedEvent(ProductId id)
        {
            // Initialization logic
        }
    }

    public class CatalogItemStockLevelUpdatedEvent : Event
    {
        public CatalogItemStockLevelUpdatedEvent(ProductId id, int newStockLevel)
        {
            // Initialization logic
        }
    }

    public class CatalogItemStatusChangedEvent : Event
    {
        public CatalogItemStatusChangedEvent(ProductId id, EntityStatus newStatus)
        {
            // Initialization logic
        }
    }
}
