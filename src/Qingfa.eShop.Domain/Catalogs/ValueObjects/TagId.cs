namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    public record TagId
    {
        /// <summary>
        /// Gets the unique identifier value for the tag.
        /// </summary>
        public int Value { get; }

        // Private constructor to ensure encapsulation and immutability
        private TagId(int value)
        {
            if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value), "Value must be positive.");
            Value = value;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TagId"/> class.
        /// </summary>
        /// <param name="value">The unique identifier value for the tag.</param>
        /// <returns>A new <see cref="TagId"/> instance.</returns>
        public static TagId Create(int value)
        {
            return new TagId(value);
        }
    }
}
