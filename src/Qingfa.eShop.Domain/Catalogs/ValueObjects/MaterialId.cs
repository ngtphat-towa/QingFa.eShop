using QingFa.EShop.Domain.DomainModels.Exceptions;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    /// <summary>
    /// Represents the unique identifier for a material.
    /// </summary>
    public record MaterialId
    {
        /// <summary>
        /// Gets the value of the material identifier.
        /// </summary>
        public int Value { get; }

        private MaterialId(int value)
        {
            if (value <= 0) throw CoreException.NullOrEmptyArgument(nameof(MaterialId));
            Value = value;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="MaterialId"/> class.
        /// </summary>
        /// <param name="value">The unique identifier value.</param>
        /// <returns>A new instance of the <see cref="MaterialId"/> class.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than or equal to zero.</exception>
        public static MaterialId Create(int value)
        {
            return new MaterialId(value);
        }
    }
}
