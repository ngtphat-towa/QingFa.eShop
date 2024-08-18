using ErrorOr;

using QingFa.EShop.Domain.DomainModels.Core;
using QingFa.EShop.Domain.DomainModels.Errors;

namespace QingFa.EShop.Domain.Commons.ValueObjects
{
    public class Price : ValueObject
    {
        public decimal Value { get; }
        public string Currency { get; }

        private Price(decimal value, string currency)
        {
            Value = value;
            Currency = currency;
        }

        public static ErrorOr<Price> Create(decimal value, string currency)
        {
            if (value < 0)
                return CoreErrors.ValidationError(nameof(value), "Price cannot be negative.");

            if (string.IsNullOrWhiteSpace(currency))
                return CoreErrors.ValidationError(nameof(currency), "Currency cannot be empty.");

            return new Price(value, currency);
        }

        public static ErrorOr<Price> CreateWithDefaultCurrency(decimal value, string defaultCurrency = "USD")
        {
            if (value < 0)
                return CoreErrors.ValidationError(nameof(value), "Price cannot be negative.");

            return new Price(value, defaultCurrency);
        }

        public static ErrorOr<Price> CreateWithDefaultValue(string currency, decimal defaultValue = 0.0m)
        {
            if (string.IsNullOrWhiteSpace(currency))
                return CoreErrors.ValidationError(nameof(currency), "Currency cannot be empty.");

            return new Price(defaultValue, currency);
        }

        public static ErrorOr<Price> CreateWithDefaults(decimal value = 0.0m, string currency = "USD")
        {
            if (value < 0)
                return CoreErrors.ValidationError(nameof(value), "Price cannot be negative.");

            if (string.IsNullOrWhiteSpace(currency))
                return CoreErrors.ValidationError(nameof(currency), "Currency cannot be empty.");

            return new Price(value, currency);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
            yield return Currency;
        }
    }
}
