using QingFa.EShop.Domain.DomainModels.Bases;

namespace QingFa.EShop.Domain.Commons.ValueObjects
{
    /// <summary>
    /// Represents a rating value in the domain model.
    /// Ratings are integers between 1 and 5.
    /// </summary>
    public class Rating : ValueObject
    {
        /// <summary>
        /// Gets the value of the rating.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rating"/> class.
        /// </summary>
        /// <param name="value">The rating value (between 1 and 5).</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="value"/> is less than 1 or greater than 5.
        /// </exception>
        private Rating(int value)
        {
            if (value < 1 || value > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Rating must be between 1 and 5");
            }
            Value = value;
        }

        /// <summary>
        /// Creates a new <see cref="Rating"/> instance.
        /// </summary>
        /// <param name="value">The rating value (between 1 and 5).</param>
        /// <returns>A new instance of <see cref="Rating"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="value"/> is less than 1 or greater than 5.
        /// </exception>
        public static Rating Create(int value)
        {
            return new Rating(value);
        }

        /// <summary>
        /// Gets the components used to determine equality for this value object.
        /// </summary>
        /// <returns>
        /// An enumeration of objects that represent the components of this value object.
        /// </returns>
        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        /// <summary>
        /// Returns a string that represents the current <see cref="Rating"/> object.
        /// </summary>
        /// <returns>
        /// A string that represents the current <see cref="Rating"/> object.
        /// </returns>
        public override string ToString()
        {
            return $"Rating({Value})";
        }
    }
}
