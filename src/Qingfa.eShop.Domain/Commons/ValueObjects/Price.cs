using QingFa.EShop.Domain.DomainModels.Bases;

namespace QingFa.EShop.Domain.Commons.ValueObjects;

public class Price : ValueObject
{
    public decimal Value { get; }
    public string Currency { get; }

    public Price(decimal value, string currency)
    {
        Value = value;
        Currency = currency;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Currency;
    }
}


