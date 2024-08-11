using QingFa.EShop.Domain.DomainModels.Exceptions;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    /// <summary>
    /// Represents the unique identifier for a care instruction.
    /// </summary>
    public record CareInstructionId
    {
        /// <summary>
        /// Gets the value of the care instruction identifier.
        /// </summary>
        public int Value { get; }

        private CareInstructionId(int value)
        {
            if (value <= 0) throw CoreException.NullOrEmptyArgument(nameof(CareInstructionId));
            Value = value;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CareInstructionId"/> class.
        /// </summary>
        /// <param name="value">The unique identifier value.</param>
        /// <returns>A new instance of the <see cref="CareInstructionId"/> class.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than or equal to zero.</exception>
        public static CareInstructionId Create(int value)
        {
            return new CareInstructionId(value);
        }
    }
}
