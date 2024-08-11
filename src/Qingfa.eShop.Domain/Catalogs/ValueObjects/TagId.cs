namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    /// <summary>
    /// Represents a unique identifier for a tag.
    /// </summary>
    public record TagId
    {
        /// <summary>
        /// Gets the value of the tag identifier.
        /// </summary>
        public int Value { get; }

        private TagId(int value)
        {
            if (value <= 0) throw new ArgumentException("Value must be greater than zero.", nameof(value));
            Value = value;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TagId"/> class.
        /// </summary>
        /// <param name="value">The unique identifier value.</param>
        /// <returns>A new <see cref="TagId"/> instance.</returns>
        public static TagId Create(int value)
        {
            return new TagId(value);
        }
    }
}
