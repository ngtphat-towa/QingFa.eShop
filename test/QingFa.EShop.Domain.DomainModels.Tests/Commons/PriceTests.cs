using FluentAssertions;

using QingFa.EShop.Domain.Commons.ValueObjects;

namespace QingFa.EShop.Domain.Tests.Commons
{
    public class PriceTests
    {
        [Fact]
        public void Create_ShouldReturnPrice_WhenValidValueAndCurrencyAreProvided()
        {
            // Arrange
            decimal value = 29.99m;
            string currency = "USD";

            // Act
            var result = Price.Create(value, currency);

            // Assert
            result.IsError.Should().BeFalse();
            var price = result.Value;
            price.Should().NotBeNull();
            price.Value.Should().Be(value);
            price.Currency.Should().Be(currency);
        }

        [Fact]
        public void Create_ShouldReturnError_WhenNegativeValueIsProvided()
        {
            // Arrange
            decimal invalidValue = -1m;
            string currency = "USD";

            // Act
            var result = Price.Create(invalidValue, currency);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Description.Contains("Price cannot be negative."));
        }

        [Fact]
        public void Create_ShouldReturnError_WhenEmptyCurrencyIsProvided()
        {
            // Arrange
            decimal value = 29.99m;
            string invalidCurrency = "";

            // Act
            var result = Price.Create(value, invalidCurrency);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Description.Contains("Currency cannot be empty."));
        }

        [Fact]
        public void Equality_ShouldReturnTrue_ForEqualPrices()
        {
            // Arrange
            var price1 = Price.Create(29.99m, "USD").Value;
            var price2 = Price.Create(29.99m, "USD").Value;

            // Act & Assert
            price1.Should().Be(price2);
        }

        [Fact]
        public void Equality_ShouldReturnFalse_ForDifferentPrices()
        {
            // Arrange
            var price1 = Price.Create(29.99m, "USD").Value;
            var price2 = Price.Create(19.99m, "USD").Value;
            var price3 = Price.Create(29.99m, "EUR").Value;

            // Act & Assert
            price1.Should().NotBe(price2);
            price1.Should().NotBe(price3);
        }

        [Fact]
        public void GetHashCode_ShouldBeSame_ForEqualPrices()
        {
            // Arrange
            var price1 = Price.Create(29.99m, "USD").Value;
            var price2 = Price.Create(29.99m, "USD").Value;

            // Act & Assert
            price1.GetHashCode().Should().Be(price2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ShouldBeDifferent_ForDifferentPrices()
        {
            // Arrange
            var price1 = Price.Create(29.99m, "USD").Value;
            var price2 = Price.Create(19.99m, "USD").Value;
            var price3 = Price.Create(29.99m, "EUR").Value;

            // Act & Assert
            price1.GetHashCode().Should().NotBe(price2.GetHashCode());
            price1.GetHashCode().Should().NotBe(price3.GetHashCode());
        }
    }
}
