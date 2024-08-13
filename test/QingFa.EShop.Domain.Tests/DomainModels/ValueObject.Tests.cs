using QingFa.EShop.Domain.DomainModels;

namespace QingFa.eShop.Domain.Tests.DomainModels;

public class ValueObjectTests
{
    private class Money : ValueObject
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return Currency;
        }
    }

    [Fact]
    public void ValueObject_Equality_ShouldCompareBasedOnValues()
    {
        var money1 = new Money(100, "USD");
        var money2 = new Money(100, "USD");
        var money3 = new Money(200, "USD");

        Assert.Equal(money1, money2);
        Assert.NotEqual(money1, money3);
    }

    [Fact]
    public void ValueObject_ShouldHaveCorrectHashCode()
    {
        var money1 = new Money(100, "USD");
        var money2 = new Money(100, "USD");

        Assert.Equal(money1.GetHashCode(), money2.GetHashCode());
    }

    [Fact]
    public void ValueObject_ShouldBeImmutable()
    {
        var money = new Money(100, "USD");
        var newMoney = new Money(200, "USD");

        Assert.Equal(100, money.Amount);
        Assert.Equal("USD", money.Currency);
        Assert.Equal(200, newMoney.Amount);
        Assert.Equal("USD", newMoney.Currency);
    }

    [Fact]
    public void ValueObject_EqualOperator_ShouldReturnTrueForEqualValues()
    {
        var money1 = new Money(100, "USD");
        var money2 = new Money(100, "USD");

        Assert.True(money1 == money2);
        Assert.False(money1 != money2);
    }

    [Fact]
    public void ValueObject_EqualOperator_ShouldReturnFalseForDifferentValues()
    {
        var money1 = new Money(100, "USD");
        var money2 = new Money(200, "USD");

        Assert.False(money1 == money2);
        Assert.True(money1 != money2);
    }
}