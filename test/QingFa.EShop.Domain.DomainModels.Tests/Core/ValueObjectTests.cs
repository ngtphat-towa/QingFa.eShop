using FluentAssertions;

using QingFa.EShop.Domain.DomainModels.Core;

namespace QingFa.EShop.Domain.Tests.Core
{
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

        private class SpecialMoney : Money
        {
            public SpecialMoney(decimal amount, string currency) : base(amount, currency) { }
        }

        [Fact]
        public void HandleEquality_WhenComparingEqualObjects_ShouldReturnTrue()
        {
            var money1 = new Money(100, "USD");
            var money2 = new Money(100, "USD");

            money1.Should().Be(money2);
        }

        [Fact]
        public void HandleEquality_WhenComparingDifferentObjects_ShouldReturnFalse()
        {
            var money1 = new Money(100, "USD");
            var money2 = new Money(200, "USD");

            money1.Should().NotBe(money2);
        }

        [Fact]
        public void HandleHashCode_WhenObjectsAreEqual_ShouldReturnSameHashCode()
        {
            var money1 = new Money(100, "USD");
            var money2 = new Money(100, "USD");

            money1.GetHashCode().Should().Be(money2.GetHashCode());
        }

        [Fact]
        public void HandleImmutability_WhenCreatingNewInstance_ShouldMaintainInitialValues()
        {
            var money = new Money(100, "USD");

            money.Amount.Should().Be(100);
            money.Currency.Should().Be("USD");
        }

        [Fact]
        public void HandleEqualOperator_WhenObjectsAreEqual_ShouldReturnTrue()
        {
            var money1 = new Money(100, "USD");
            var money2 = new Money(100, "USD");

            (money1 == money2).Should().BeTrue();
            (money1 != money2).Should().BeFalse();
        }

        [Fact]
        public void HandleEqualOperator_WhenObjectsAreDifferent_ShouldReturnFalse()
        {
            var money1 = new Money(100, "USD");
            var money2 = new Money(200, "USD");

            (money1 == money2).Should().BeFalse();
            (money1 != money2).Should().BeTrue();
        }

        [Fact]
        public void HandleEquality_WhenComparingWithNull_ShouldReturnFalse()
        {
            var money = new Money(100, "USD");

            money.Equals(null).Should().BeFalse();
        }

        [Fact]
        public void HandleEquality_WhenComparingWithDifferentType_ShouldReturnFalse()
        {
            var money = new Money(100, "USD");
            var differentType = new object();

            money.Equals(differentType).Should().BeFalse();
        }

        [Fact]
        public void HandleEquality_WhenCurrencyIsEmpty_ShouldBeEqual()
        {
            var money1 = new Money(100, "");
            var money2 = new Money(100, "");

            money1.Should().Be(money2);
        }

        [Fact]
        public void HandleEquality_WhenDefaultValues_ShouldBeEqual()
        {
            var money1 = new Money(0, "USD");
            var money2 = new Money(0, "USD");

            money1.Should().Be(money2);
        }

        [Fact]
        public void HandleEquality_WhenComparingWithInheritedClass_ShouldBeEqual()
        {
            // Arrange
            var money = new Money(100, "USD");
            var specialMoney = new SpecialMoney(100, "USD");

            // Act & Assert
            money.Should().Be(specialMoney);  // Uses the overridden Equals method
            (money == specialMoney).Should().BeTrue();
            (money != specialMoney).Should().BeFalse();

            // Ensure that the hash codes are the same for both
            money.GetHashCode().Should().Be(specialMoney.GetHashCode());
        }

        [Fact]
        public void HandleHashCode_WhenCalledMultipleTimes_ShouldReturnSameValue()
        {
            var money = new Money(100, "USD");
            var hashCode1 = money.GetHashCode();
            var hashCode2 = money.GetHashCode();

            hashCode1.Should().Be(hashCode2);
        }
    }
}
