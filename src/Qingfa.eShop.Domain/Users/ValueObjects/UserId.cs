namespace QingFa.EShop.Domain.Users.ValueObjects
{
    /// <summary>
    /// Represents a unique identifier for a user.
    /// </summary>
    /// <remarks>
    /// This value object encapsulates a unique integer identifier for a user. 
    /// The identifier must be a positive integer.
    /// </remarks>
    public record UserId
    {
        /// <summary>
        /// Gets the unique identifier value for the user.
        /// </summary>
        /// <value>
        /// The unique identifier value, which must be a positive integer.
        /// </value>
        public int Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserId"/> record.
        /// </summary>
        /// <param name="value">The unique identifier value for the user.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is less than or equal to zero.</exception>
        private UserId(int value)
        {
            if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value), "Value must be positive.");
            Value = value;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="UserId"/> record.
        /// </summary>
        /// <param name="value">The unique identifier value for the user.</param>
        /// <returns>A new <see cref="UserId"/> instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is less than or equal to zero.</exception>
        public static UserId Create(int value)
        {
            return new UserId(value);
        }
    }
}
