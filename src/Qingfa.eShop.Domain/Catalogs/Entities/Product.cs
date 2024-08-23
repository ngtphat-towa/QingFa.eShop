using QingFa.EShop.Domain.Core.Entities;
using QingFa.EShop.Domain.Core.Exceptions;
using QingFa.EShop.Domain.Common.ValueObjects;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    public class Product : AuditEntity
    {
        // Properties
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public string? LongDescription { get; set; }
        public Money Price { get; private set; }
        public string SKU { get; set; }
        public int StockLevel { get; set; }
        public Guid? BrandId { get; private set; }
        public virtual Brand? Brand { get; private set; }
        public virtual ICollection<CategoryProduct> CategoryProducts { get; private set; }
        public virtual ICollection<ProductVariant> ProductVariants { get; private set; }

        // Private parameterless constructor for EF Core
#pragma warning disable CS8618 
        private Product()
#pragma warning restore CS8618 
            : base(Guid.NewGuid())
        {
            CategoryProducts = new HashSet<CategoryProduct>();
            ProductVariants = new HashSet<ProductVariant>();
        }

        // Private constructor for internal use
        private Product(Guid id, string name, Money price, string sku, Guid brandId, string? description = null)
            : base(id)
        {
            Name = name ?? throw CoreException.NullArgument(nameof(name));
            Price = price ?? throw CoreException.NullArgument(nameof(price));
            SKU = sku ?? throw CoreException.NullArgument(nameof(sku));
            BrandId = brandId;
            Description = description;
            CategoryProducts = new HashSet<CategoryProduct>();
            ProductVariants = new HashSet<ProductVariant>();
        }

        // Factory method to create a new product
        public static Product Create(string name, Money price, string sku, Guid brandId, string? description = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw CoreException.NullOrEmptyArgument(nameof(name));
            if (price == null)
                throw CoreException.NullArgument(nameof(price));
            if (string.IsNullOrWhiteSpace(sku))
                throw CoreException.NullOrEmptyArgument(nameof(sku));

            return new Product(Guid.NewGuid(), name, price, sku, brandId, description);
        }

        // Method to update product details
        public void Update(string name, Money price, string sku, Guid brandId, string? description = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw CoreException.NullOrEmptyArgument(nameof(name));
            if (price == null)
                throw CoreException.NullArgument(nameof(price));
            if (string.IsNullOrWhiteSpace(sku))
                throw CoreException.NullOrEmptyArgument(nameof(sku));

            Name = name;
            Price = price;
            SKU = sku;
            BrandId = brandId;
            Description = description;
        }

        // Method to add a category
        public void AddCategory(CategoryProduct categoryProduct)
        {
            if (categoryProduct == null)
                throw CoreException.NullArgument(nameof(categoryProduct));

            CategoryProducts.Add(categoryProduct);
        }

        // Method to remove a category
        public void RemoveCategory(CategoryProduct categoryProduct)
        {
            if (categoryProduct == null)
                throw CoreException.NullArgument(nameof(categoryProduct));

            CategoryProducts?.Remove(categoryProduct);
        }

        // Method to add a product variant
        public void AddVariant(ProductVariant variant)
        {
            if (variant == null)
                throw CoreException.NullArgument(nameof(variant));

            ProductVariants.Add(variant);
        }

        // Method to remove a product variant
        public void RemoveVariant(ProductVariant variant)
        {
            if (variant == null)
                throw CoreException.NullArgument(nameof(variant));

            ProductVariants?.Remove(variant);
        }
    }
}
