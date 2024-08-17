using QingFa.EShop.Domain.Catalogs.Events;
using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.Common.Enums;
using QingFa.EShop.Domain.Common.ValueObjects;
using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.Aggregates
{
    public class Product : AggregateRoot<ProductId>
    {
        public string Name { get;  set; }
        public string ShortDescription { get;  set; }
        public string Description { get;  set; }
        public CategoryId CategoryId { get;  set; }
        public BrandId BrandId { get;  set; }
        public Price Price { get;  set; }
        public string SKU { get;  set; }
        public int StockLevel { get;  set; }
        public EntityStatus Status { get;  set; }

        // Constructor for creating a new CatalogItem
        public Product(ProductId id, string name, string shortDescription, string description,
                           CategoryId categoryId, BrandId brandId, Price price, string sku, int stockLevel,
                           EntityStatus status)
            : base(id)
        {
            Name = name;
            ShortDescription = shortDescription;
            Description = description;
            CategoryId = categoryId;
            BrandId = brandId;
            Price = price;
            SKU = sku;
            StockLevel = stockLevel;
            Status = status;

            // Optionally, add a domain event for item creation
            AddDomainEvent(new CatalogItemCreatedEvent(id));
        }

#pragma warning disable CS8618 
        protected Product() :base(default!)
#pragma warning restore CS8618 
        {
        }

        // Method to update item details
        public void UpdateDetails(string name, string shortDescription, string description,
                                  CategoryId categoryId, BrandId brandId, Price price,
                                  string sku, int stockLevel)
        {
            Name = name;
            ShortDescription = shortDescription;
            Description = description;
            CategoryId = categoryId;
            BrandId = brandId;
            Price = price;
            SKU = sku;
            StockLevel = stockLevel;
            UpdateTimestamp();

            // Optionally, add a domain event for item update
            AddDomainEvent(new CatalogItemUpdatedEvent(Id));
        }

        // Method to update stock level
        public void UpdateStockLevel(int newStockLevel)
        {
            StockLevel = newStockLevel;
            UpdateTimestamp();

            // Optionally, add a domain event for stock level change
            AddDomainEvent(new CatalogItemStockLevelUpdatedEvent(Id, newStockLevel));
        }

        // Method to change the status of the item
        public void ChangeStatus(EntityStatus newStatus)
        {
            Status = newStatus;
            UpdateTimestamp();

            // Optionally, add a domain event for status change
            AddDomainEvent(new CatalogItemStatusChangedEvent(Id, newStatus));
        }

        // Method to validate the item (basic example)
         void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("Name cannot be empty");

            if (Price == null)
                throw new ArgumentException("Price cannot be null");

            if (StockLevel < 0)
                throw new ArgumentException("Stock level cannot be negative");

            // Add more validation as needed
        }
    }

}
