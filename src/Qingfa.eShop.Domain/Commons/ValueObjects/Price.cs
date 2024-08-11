using QingFa.EShop.Domain.DomainModels.Bases;

namespace QingFa.EShop.Domain.Commons.ValueObjects
{
    /// <summary>
    /// Represents a monetary value with a specific currency.
    /// </summary>
    public class Price : ValueObject
    {
        /// <summary>
        /// Gets the value of the monetary amount.
        /// </summary>
        public decimal Value { get; }

        /// <summary>
        /// Gets the currency of the monetary amount.
        /// </summary>
        public string Currency { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Price"/> class.
        /// </summary>
        /// <param name="value">The monetary amount.</param>
        /// <param name="currency">The currency of the monetary amount.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="currency"/> is null or whitespace.</exception>
        public Price(decimal value, string currency)
        {
            if (string.IsNullOrWhiteSpace(currency))
            {
                throw new ArgumentException("Currency cannot be null or whitespace.", nameof(currency));
            }

            Value = value;
            Currency = currency;
        }

        /// <summary>
        /// Adds two <see cref="Price"/> values together. Assumes both prices are in the same currency.
        /// </summary>
        /// <param name="other">The other <see cref="Price"/> to add.</param>
        /// <returns>A new <see cref="Price"/> that represents the sum of the two prices.</returns>
        /// <exception cref="InvalidOperationException">Thrown when currencies do not match.</exception>
        public Price Add(Price other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (Currency != other.Currency)
            {
                throw new InvalidOperationException("Cannot add prices with different currencies.");
            }

            return new Price(Value + other.Value, Currency);
        }

        /// <summary>
        /// Subtracts one <see cref="Price"/> from another. Assumes both prices are in the same currency.
        /// </summary>
        /// <param name="other">The other <see cref="Price"/> to subtract.</param>
        /// <returns>A new <see cref="Price"/> that represents the difference between the two prices.</returns>
        /// <exception cref="InvalidOperationException">Thrown when currencies do not match.</exception>
        public Price Subtract(Price other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (Currency != other.Currency)
            {
                throw new InvalidOperationException("Cannot subtract prices with different currencies.");
            }

            return new Price(Value - other.Value, Currency);
        }

        /// <summary>
        /// Converts the <see cref="Price"/> to a string representation.
        /// </summary>
        /// <returns>A string representation of the <see cref="Price"/>.</returns>
        public override string ToString()
        {
            return $"{Value:C} {Currency}";
        }

        /// <summary>
        /// Provides equality comparison for <see cref="Price"/> objects.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of objects used for equality comparison.</returns>
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Currency;
        }
    }
}
