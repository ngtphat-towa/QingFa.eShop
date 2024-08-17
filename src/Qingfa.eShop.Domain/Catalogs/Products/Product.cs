using ErrorOr;
using QingFa.EShop.Domain.DomainModels.Core;
using QingFa.EShop.Domain.DomainModels.Errors;
using QingFa.EShop.Domain.Catalogs.Categories;
using QingFa.EShop.Domain.Catalogs.Brands;

namespace QingFa.EShop.Domain.Catalogs.Products
{
    public class Product : Entity<ProductId>
    {
        #region Properties

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public CategoryId CategoryId { get; private set; }
        public BrandId BrandId { get; private set; }
        public int StockQuantity { get; private set; }
        public bool IsActive { get; private set; }

        #endregion

        #region Constructors

        protected Product(
            ProductId id,
            string name,
            string description,
            decimal price,
            CategoryId categoryId,
            BrandId brandId,
            int stockQuantity,
            bool isActive
        ) : base(id)
        {
            Name = name;
            Description = description;
            Price = price;
            CategoryId = categoryId;
            BrandId = brandId;
            StockQuantity = stockQuantity;
            IsActive = isActive;
        }

#pragma warning disable CS8618 
        protected Product() : base(default!) { }
#pragma warning restore CS8618 

        #endregion

        #region Factory Methods

        public static ErrorOr<Product> Create(
            ProductId id,
            string name,
            string description,
            decimal price,
            CategoryId categoryId,
            BrandId brandId,
            int stockQuantity,
            bool isActive
        )
        {
            if (string.IsNullOrWhiteSpace(name))
                return CoreErrors.ValidationError(nameof(name), "Name cannot be empty.");

            if (price < 0)
                return CoreErrors.ValidationError(nameof(price), "Price cannot be negative.");

            if (stockQuantity < 0)
                return CoreErrors.ValidationError(nameof(stockQuantity), "StockQuantity cannot be negative.");

            return new Product(id, name, description, price, categoryId, brandId, stockQuantity, isActive);
        }

        #endregion

        #region Methods

        public void UpdateDetails(
            string name,
            string description,
            decimal price,
            CategoryId categoryId,
            BrandId brandId,
            int stockQuantity,
            bool isActive
        )
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            if (price < 0)
                throw new ArgumentException("Price cannot be negative.", nameof(price));

            if (stockQuantity < 0)
                throw new ArgumentException("StockQuantity cannot be negative.", nameof(stockQuantity));

            Name = name;
            Description = description;
            Price = price;
            CategoryId = categoryId;
            BrandId = brandId;
            StockQuantity = stockQuantity;
            IsActive = isActive;
        }

        #endregion
    }
}
