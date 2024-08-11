using QingFa.EShop.Domain.DomainModels.Exceptions;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    /// <summary>
    /// Represents the unique identifier for a catalog sub-category.
    /// </summary>
    public record CatalogSubCategoryId
    {
        /// <summary>
        /// Gets the value of the catalog sub-category identifier.
        /// </summary>
        public int Value { get; }

        private CatalogSubCategoryId(int value)
        {
            if (value <= 0) throw CoreException.NullOrEmptyArgument(nameof(CatalogSubCategoryId));
            Value = value;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CatalogSubCategoryId"/> class.
        /// </summary>
        /// <param name="value">The unique identifier value.</param>
        /// <returns>A new instance of the <see cref="CatalogSubCategoryId"/> class.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than or equal to zero.</exception>
        public static CatalogSubCategoryId Create(int value)
        {
            return new CatalogSubCategoryId(value);
        }
    }
}
