using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Common.ValueObjects
{
    public sealed class Price : ValueObject
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }

        public Price(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        private Price()
        {
            Amount = decimal.Zero;
            Currency = string.Empty;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Currency;
            yield return Amount;
        }
    }
}
