using QingFa.EShop.Domain.DomainModels.Exceptions;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    /// <summary>
    /// Represents the unique identifier for an age group.
    /// </summary>
    public record AgeGroupId
    {
        /// <summary>
        /// Gets the value of the age group identifier.
        /// </summary>
        public int Value { get; }

        private AgeGroupId(int value)
        {
            if (value <= 0) throw CoreException.NullOrEmptyArgument(nameof(AgeGroupId));
            Value = value;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AgeGroupId"/> class.
        /// </summary>
        /// <param name="value">The unique identifier value.</param>
        /// <returns>A new instance of the <see cref="AgeGroupId"/> class.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than or equal to zero.</exception>
        public static AgeGroupId Create(int value)
        {
            return new AgeGroupId(value);
        }
    }
}
