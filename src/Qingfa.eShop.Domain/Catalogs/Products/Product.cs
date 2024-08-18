using ErrorOr;
using QingFa.EShop.Domain.DomainModels.Core;
using QingFa.EShop.Domain.DomainModels.Errors;
using QingFa.EShop.Domain.Catalogs.Categories;
using QingFa.EShop.Domain.Catalogs.Brands;

namespace QingFa.EShop.Domain.Catalogs.Products
{
    public class Product : AggregateRoot<ProductId>
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

        // Fully parameterized constructor - protected to restrict access
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

        // Parameterless constructor for EF Core
#pragma warning disable CS8618
        protected Product() : base(default!)
#pragma warning restore CS8618
        {
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method to create a Product with error handling.
        /// </summary>
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
            var validationErrors = ValidateProductDetails(name, price, stockQuantity);
            if (validationErrors.Count > 0)
            {
                return validationErrors;
            }

            var product = new Product(
                id,
                name,
                description,
                price,
                categoryId,
                brandId,
                stockQuantity,
                isActive
            );

            return product;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method to update the Product details.
        /// </summary>
        public ErrorOr<Product> UpdateDetails(
            string name,
            string description,
            decimal price,
            CategoryId categoryId,
            BrandId brandId,
            int stockQuantity,
            bool isActive
        )
        {
            var validationErrors = ValidateProductDetails(name, price, stockQuantity);
            if (validationErrors.Count > 0)
            {
                return validationErrors;
            }

            Name = name;
            Description = description;
            Price = price;
            CategoryId = categoryId;
            BrandId = brandId;
            StockQuantity = stockQuantity;
            IsActive = isActive;

            return this;
        }

        /// <summary>
        /// Method to toggle the active status of the Product.
        /// </summary>
        public void ToggleActiveStatus()
        {
            IsActive = !IsActive;
        }

        /// <summary>
        /// Method to provide a summary of the Product.
        /// </summary>
        public string GetSummary()
        {
            return $"Product: {Name}, Price: {Price}, Stock: {StockQuantity}, Active: {IsActive}";
        }

        #endregion

        #region Private Methods

        private static List<Error> ValidateProductDetails(string name, decimal price, int stockQuantity)
        {
            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(name))
            {
                errors.Add(CoreErrors.ValidationError(nameof(Name), "Name cannot be empty."));
            }

            if (price < 0)
            {
                errors.Add(CoreErrors.ValidationError(nameof(Price), "Price cannot be negative."));
            }

            if (stockQuantity < 0)
            {
                errors.Add(CoreErrors.ValidationError(nameof(StockQuantity), "StockQuantity cannot be negative."));
            }

            return errors;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Product other)
                return false;

            return Id.Equals(other.Id) &&
                   Name == other.Name &&
                   Description == other.Description &&
                   Price == other.Price &&
                   CategoryId.Equals(other.CategoryId) &&
                   BrandId.Equals(other.BrandId) &&
                   StockQuantity == other.StockQuantity &&
                   IsActive == other.IsActive;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Description, Price, CategoryId, BrandId, StockQuantity, IsActive);
        }

        #endregion
    }
}
