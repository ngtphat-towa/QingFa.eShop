using QingFa.EShop.Domain.DomainModels.Exceptions;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    /// <summary>
    /// Represents the unique identifier for a catalog brand.
    /// </summary>
    public record CatalogBrandId
    {
        /// <summary>
        /// Gets the value of the catalog brand identifier.
        /// </summary>
        public int Value { get; }

        private CatalogBrandId(int value)
        {
            if (value <= 0) throw CoreException.NullOrEmptyArgument(nameof(CatalogBrandId));
            Value = value;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CatalogBrandId"/> class.
        /// </summary>
        /// <param name="value">The unique identifier value.</param>
        /// <returns>A new instance of the <see cref="CatalogBrandId"/> class.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than or equal to zero.</exception>
        public static CatalogBrandId Create(int value)
        {
            return new CatalogBrandId(value);
        }
    }
}
