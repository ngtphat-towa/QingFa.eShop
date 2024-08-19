using QingFa.EShop.Domain.Core.ValueObjects;

namespace QingFa.EShop.Domain.Common.ValueObjects
{
    public class Money : ValueObject
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}
