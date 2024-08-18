using ErrorOr;

using FluentAssertions;

using QingFa.EShop.Domain.Catalogs.Products;
using QingFa.EShop.Domain.Catalogs.Variants;
using QingFa.EShop.Domain.Commons.ValueObjects;
using QingFa.EShop.Domain.DomainModels.Errors;

using Xunit;

namespace QingFa.EShop.Domain.Tests.Catalogs
{
    public class ProductVariantTests
    {
        private static ProductVariantId CreateValidProductVariantId() => new ProductVariantId(1);
        private static ProductId CreateValidProductId() => new ProductId(1);

        [Fact]
        public void CreateWithDefaults_ShouldReturnProductVariant_WhenValidParametersAreProvided()
        {
            // Arrange
            var id = CreateValidProductVariantId();
            var productId = CreateValidProductId();
            var sku = "SKU123";
            var priceValue = 29.99m;
            var currency = "USD";
            var stockQuantity = 100;
            var isActive = true;

            // Act
            var result = ProductVariant.CreateWithDefaults(id, productId, sku, priceValue, currency, stockQuantity, isActive);

            // Assert
            result.IsError.Should().BeFalse();
            var productVariant = result.Value;
            productVariant.Should().NotBeNull();
            productVariant.SKU.Should().Be(sku);
            productVariant.Price.Value.Should().Be(priceValue);
            productVariant.Price.Currency.Should().Be(currency);
            productVariant.StockQuantity.Should().Be(stockQuantity);
            productVariant.IsActive.Should().Be(isActive);
            productVariant.ProductId.Should().Be(productId);
        }

        [Fact]
        public void CreateWithPrice_ShouldReturnProductVariant_WhenValidParametersAreProvided()
        {
            // Arrange
            var id = CreateValidProductVariantId();
            var productId = CreateValidProductId();
            var sku = "SKU123";
            var price = Price.Create(29.99m, "USD").Value; // Create a Price object
            var stockQuantity = 100;
            var isActive = true;

            // Act
            var result = ProductVariant.CreateWithPrice(id, productId, sku, price, stockQuantity, isActive);

            // Assert
            result.IsError.Should().BeFalse();
            var productVariant = result.Value;
            productVariant.Should().NotBeNull();
            productVariant.SKU.Should().Be(sku);
            productVariant.Price.Should().Be(price);
            productVariant.StockQuantity.Should().Be(stockQuantity);
            productVariant.IsActive.Should().Be(isActive);
            productVariant.ProductId.Should().Be(productId);
        }

        [Theory]
        [InlineData("", 29.99, "USD", 100, true, "SKU cannot be empty.")]
        [InlineData("SKU123", -1, "USD", 100, true, "Price cannot be negative.")]
        [InlineData("SKU123", 29.99, "", 100, true, "Currency cannot be empty.")]
        [InlineData("SKU123", 29.99, "USD", -1, true, "StockQuantity cannot be negative.")]
        public void CreateWithDefaults_ShouldReturnError_WhenInvalidParametersAreProvided(
            string sku,
            decimal priceValue,
            string currency,
            int stockQuantity,
            bool isActive,
            string expectedErrorMessage)
        {
            // Arrange
            var id = CreateValidProductVariantId();
            var productId = CreateValidProductId();

            // Act
            var result = ProductVariant.CreateWithDefaults(id, productId, sku, priceValue, currency, stockQuantity, isActive);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Description.Contains(expectedErrorMessage));
        }

        [Theory]
        [InlineData("", 29.99, "USD", 100, true, "SKU cannot be empty.")]
        [InlineData("SKU123", -1, "USD", 100, true, "Price cannot be negative.")]
        [InlineData("SKU123", 29.99, "", 100, true, "Currency cannot be empty.")]
        [InlineData("SKU123", 29.99, "USD", -1, true, "StockQuantity cannot be negative.")]
        public void CreateWithPrice_ShouldReturnError_WhenInvalidParametersAreProvided(
            string sku,
            decimal priceValue,
            string currency,
            int stockQuantity,
            bool isActive,
            string expectedErrorMessage)
        {
            // Arrange
            var id = CreateValidProductVariantId();
            var productId = CreateValidProductId();
            var priceResult = Price.Create(priceValue, currency);

            // Act
            var result = priceResult.IsError
                ? priceResult.Errors
                : ProductVariant.CreateWithPrice(id, productId, sku, priceResult.Value, stockQuantity, isActive);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Description.Contains(expectedErrorMessage));
        }

        [Fact]
        public void UpdateDetails_ShouldUpdateProductVariant_WhenValidParametersAreProvided()
        {
            // Arrange
            var id = CreateValidProductVariantId();
            var productId = CreateValidProductId();
            var initialPrice = Price.Create(10.00m, "USD").Value;
            var initialVariant = ProductVariant.CreateWithPrice(id, productId, "InitialSKU", initialPrice, 50, true).Value;

            var newSku = "UpdatedSKU";
            var newPriceValue = 19.99m;
            var newCurrency = "USD";
            var newStockQuantity = 200;
            var newIsActive = false;

            // Act
            var result = initialVariant.UpdateDetails(newSku, newPriceValue, newCurrency, newStockQuantity, newIsActive);

            // Assert
            result.IsError.Should().BeFalse();
            var updatedVariant = initialVariant;
            updatedVariant.SKU.Should().Be(newSku);
            updatedVariant.Price.Value.Should().Be(newPriceValue);
            updatedVariant.Price.Currency.Should().Be(newCurrency);
            updatedVariant.StockQuantity.Should().Be(newStockQuantity);
            updatedVariant.IsActive.Should().Be(newIsActive);
        }

        [Theory]
        [InlineData("", 19.99, "USD", -1, false, "SKU cannot be empty.")]
        [InlineData("UpdatedSKU", -1, "USD", 200, false, "Price cannot be negative.")]
        [InlineData("UpdatedSKU", 19.99, "", 200, false, "Currency cannot be empty.")]
        [InlineData("UpdatedSKU", 19.99, "USD", -1, false, "StockQuantity cannot be negative.")]
        public void UpdateDetails_ShouldReturnError_WhenInvalidParametersAreProvided(
            string sku,
            decimal priceValue,
            string currency,
            int stockQuantity,
            bool isActive,
            string expectedErrorMessage)
        {
            // Arrange
            var id = CreateValidProductVariantId();
            var productId = CreateValidProductId();
            var initialPrice = Price.Create(10.00m, "USD").Value;
            var initialVariant = ProductVariant.CreateWithPrice(id, productId, "InitialSKU", initialPrice, 50, true).Value;

            // Act
            var result = initialVariant.UpdateDetails(sku, priceValue, currency, stockQuantity, isActive);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Description.Contains(expectedErrorMessage));
        }

        [Fact]
        public void Equality_ShouldReturnTrue_ForEqualProductVariants()
        {
            // Arrange
            var id = CreateValidProductVariantId();
            var productId = CreateValidProductId();
            var price = Price.Create(29.99m, "USD").Value;
            var variant1 = ProductVariant.CreateWithPrice(id, productId, "SKU123", price, 100, true).Value;
            var variant2 = ProductVariant.CreateWithPrice(id, productId, "SKU123", price, 100, true).Value;

            // Act & Assert
            variant1.Should().Be(variant2);
        }

        [Fact]
        public void Equality_ShouldReturnFalse_ForDifferentProductVariants()
        {
            // Arrange
            var id1 = CreateValidProductVariantId();
            var id2 = new ProductVariantId(2); // Different ID
            var productId1 = CreateValidProductId();
            var productId2 = new ProductId(2); // Different ProductId
            var price1 = Price.Create(29.99m, "USD").Value;
            var price2 = Price.Create(19.99m, "EUR").Value;
            var variant1 = ProductVariant.CreateWithPrice(id1, productId1, "SKU123", price1, 100, true).Value;
            var variant2 = ProductVariant.CreateWithPrice(id2, productId2, "SKU456", price2, 50, false).Value;

            // Act & Assert
            variant1.Should().NotBe(variant2);
        }

        [Fact]
        public void GetHashCode_ShouldBeSame_ForEqualProductVariants()
        {
            // Arrange
            var id = CreateValidProductVariantId();
            var productId = CreateValidProductId();
            var price = Price.Create(29.99m, "USD").Value;
            var variant1 = ProductVariant.CreateWithPrice(id, productId, "SKU123", price, 100, true).Value;
            var variant2 = ProductVariant.CreateWithPrice(id, productId, "SKU123", price, 100, true).Value;

            // Act & Assert
            variant1.GetHashCode().Should().Be(variant2.GetHashCode());
        }
    }
}
