namespace QingFa.EShop.Domain.Reviews.ValueObjects
{
    /// <summary>
    /// Represents a unique identifier for a review.
    /// </summary>
    /// <remarks>
    /// This value object encapsulates a unique long integer identifier for a review.
    /// The identifier must be a positive long integer.
    /// </remarks>
    public record ReviewId
    {
        /// <summary>
        /// Gets the unique identifier value for the review.
        /// </summary>
        /// <value>
        /// The unique identifier value, which must be a positive long integer.
        /// </value>
        public long Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewId"/> record.
        /// </summary>
        /// <param name="value">The unique identifier value for the review.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is less than or equal to zero.</exception>
        private ReviewId(long value)
        {
            if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value), "Value must be positive.");
            Value = value;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ReviewId"/> record.
        /// </summary>
        /// <param name="value">The unique identifier value for the review.</param>
        /// <returns>A new <see cref="ReviewId"/> instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is less than or equal to zero.</exception>
        public static ReviewId Create(long value)
        {
            return new ReviewId(value);
        }
    }
}
