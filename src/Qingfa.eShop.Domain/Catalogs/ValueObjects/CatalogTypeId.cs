namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    public record CatalogTypeId
    {
        /// <summary>
        /// Gets the unique identifier value for the color.
        /// </summary>
        public int Value { get; }

        // Private constructor to ensure encapsulation and immutability
        private CatalogTypeId(int value)
        {
            if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value), "Value must be positive.");
            Value = value;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CatalogItemId"/> class.
        /// </summary>
        /// <param name="value">The unique identifier value for the color.</param>
        /// <returns>A new <see cref="CatalogItemId"/> instance.</returns>
        public static CatalogTypeId Create(int value)
        {
            return new CatalogTypeId(value);
        }
    }
}
