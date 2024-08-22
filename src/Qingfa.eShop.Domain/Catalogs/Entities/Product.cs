using QingFa.EShop.Domain.Common.ValueObjects;
using QingFa.EShop.Domain.Core.Entities;
using QingFa.EShop.Domain.Core.Exceptions;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    public class Product : AuditEntity
    {
        // Properties
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public Money Price { get; private set; }
        public Guid? BrandId { get; private set; }
        public virtual Brand? Brand { get; private set; } = default!;
        public virtual ICollection<CategoryProduct>? CategoryProducts { get; private set; }

        // Private parameterless constructor for EF Core
#pragma warning disable CS8618 
        private Product()
#pragma warning restore CS8618 
            : base(Guid.NewGuid())
        {
            CategoryProducts = new HashSet<CategoryProduct>();
        }

        // Private constructor for internal use
        private Product(Guid id, string name, Money price, Guid brandId, string? description = null)
            : base(id)
        {
            Name = name ?? throw CoreException.NullArgument(nameof(name));
            Price = price ?? throw CoreException.NullArgument(nameof(price));
            BrandId = brandId;
            Description = description;
            CategoryProducts = new HashSet<CategoryProduct>();
        }

        // Factory method to create a new product
        public static Product Create(string name, Money price, Guid brandId, string? description = null)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Product name cannot be null or whitespace.", nameof(name));
            if (price == null) throw CoreException.NullArgument(nameof(price));

            return new Product(Guid.NewGuid(), name, price, brandId, description);
        }

        // Method to update product details
        public void Update(string name, Money price, Guid brandId, string? description = null)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Product name cannot be null or whitespace.", nameof(name));
            if (price == null) throw CoreException.NullArgument(nameof(price));

            Name = name;
            Price = price;
            BrandId = brandId;
            Description = description;
        }

        // Method to add a category
        public void AddCategory(CategoryProduct categoryProduct)
        {
            if (categoryProduct == null) throw CoreException.NullArgument(nameof(categoryProduct));
            if (CategoryProducts != null)
            {
                CategoryProducts.Add(categoryProduct);
            }
            else
            {
                CategoryProducts = [categoryProduct];
            }
        }

        // Method to remove a category
        public void RemoveCategory(CategoryProduct categoryProduct)
        {
            if (categoryProduct == null) throw CoreException.NullArgument(nameof(categoryProduct));

            // Ensure CategoryProducts is initialized
            if (CategoryProducts != null)
            {
                CategoryProducts.Remove(categoryProduct);
            }
        }

    }
}
