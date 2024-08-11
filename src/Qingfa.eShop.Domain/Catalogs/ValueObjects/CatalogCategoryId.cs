using QingFa.EShop.Domain.DomainModels.Exceptions;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    /// <summary>
    /// Represents the unique identifier for a catalog category.
    /// </summary>
    public record CatalogCategoryId
    {
        /// <summary>
        /// Gets the value of the catalog category identifier.
        /// </summary>
        public int Value { get; }

        private CatalogCategoryId(int value)
        {
            if (value <= 0) throw CoreException.NullOrEmptyArgument(nameof(CatalogCategoryId));
            Value = value;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CatalogCategoryId"/> class.
        /// </summary>
        /// <param name="value">The unique identifier value.</param>
        /// <returns>A new instance of the <see cref="CatalogCategoryId"/> class.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than or equal to zero.</exception>
        public static CatalogCategoryId Create(int value)
        {
            return new CatalogCategoryId(value);
        }
    }
}
