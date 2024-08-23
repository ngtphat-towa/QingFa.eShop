using QingFa.EShop.Domain.Core.ValueObjects;

namespace QingFa.EShop.Domain.Common.ValueObjects
{
    public class Money : ValueObject
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; } = "USD";

        // Static properties for default and empty values
        public static readonly Money Default = new(1.00m, "USD");
        public static readonly Money Empty = new(0.00m, "USD");

        // Private constructor to enforce creation through the Create method
        private Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        // Static method to create an instance of Money
        public static Money Create(decimal amount, string currency)
        {
            if (string.IsNullOrWhiteSpace(currency))
            {
                throw new ArgumentException("Currency cannot be null or whitespace.", nameof(currency));
            }

            return new Money(amount, currency);
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}
