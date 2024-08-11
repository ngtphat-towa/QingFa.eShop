using QingFa.EShop.Domain.DomainModels.Exceptions;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    /// <summary>
    /// Represents the unique identifier for a catalog type.
    /// </summary>
    public class CatalogTypeId
    {
        /// <summary>
        /// Gets the value of the catalog type identifier.
        /// </summary>
        public int Value { get; }

        private CatalogTypeId(int value)
        {
            if (value <= 0) throw CoreException.NullOrEmptyArgument(nameof(CatalogTypeId));
            Value = value;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CatalogTypeId"/> class.
        /// </summary>
        /// <param name="value">The unique identifier value.</param>
        /// <returns>A new instance of the <see cref="CatalogTypeId"/> class.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than or equal to zero.</exception>
        public static CatalogTypeId Create(int value)
        {
            return new CatalogTypeId(value);
        }
    }
}
