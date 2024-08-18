using FluentAssertions;
using QingFa.EShop.Domain.Catalogs.Categories;
using QingFa.EShop.Domain.Catalogs.Brands;
using QingFa.EShop.Domain.Catalogs.Products;

namespace QingFa.EShop.Domain.Tests.Catalogs
{
    public class ProductTests
    {
        private static ProductId CreateValidProductId() => new ProductId(1);
        private static CategoryId CreateValidCategoryId() => new CategoryId(1);
        private static BrandId CreateValidBrandId() => new BrandId(1);

        [Fact]
        public void Create_ShouldReturnProduct_WhenValidParametersAreProvided()
        {
            // Arrange
            var id = CreateValidProductId();
            var name = "Product Name";
            var description = "Product Description";
            var price = 100m;
            var categoryId = CreateValidCategoryId();
            var brandId = CreateValidBrandId();
            var stockQuantity = 10;
            var isActive = true;

            // Act
            var result = Product.Create(id, name, description, price, categoryId, brandId, stockQuantity, isActive);

            // Assert
            result.IsError.Should().BeFalse();
            var product = result.Value;
            product.Should().NotBeNull();
            product.Name.Should().Be(name);
            product.Description.Should().Be(description);
            product.Price.Should().Be(price);
            product.CategoryId.Should().Be(categoryId);
            product.BrandId.Should().Be(brandId);
            product.StockQuantity.Should().Be(stockQuantity);
            product.IsActive.Should().Be(isActive);
        }

        [Theory]
        [InlineData("", 100, 10, "Name cannot be empty.")]
        [InlineData("Name", -1, 10, "Price cannot be negative.")]
        [InlineData("Name", 100, -1, "StockQuantity cannot be negative.")]
        public void Create_ShouldReturnError_WhenInvalidParametersAreProvided(
            string name,
            decimal price,
            int stockQuantity,
            string expectedErrorMessage)
        {
            // Arrange
            var id = CreateValidProductId();
            var description = "Product Description";
            var categoryId = CreateValidCategoryId();
            var brandId = CreateValidBrandId();
            var isActive = true;

            // Act
            var result = Product.Create(id, name, description, price, categoryId, brandId, stockQuantity, isActive);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Description.Contains(expectedErrorMessage));
        }

        [Fact]
        public void UpdateDetails_ShouldReturnUpdatedProduct_WhenValidParametersAreProvided()
        {
            // Arrange
            var id = CreateValidProductId();
            var initialProduct = Product.Create(id, "Initial Name", "Initial Description", 100m, CreateValidCategoryId(), CreateValidBrandId(), 10, true).Value;

            var newName = "Updated Name";
            var newDescription = "Updated Description";
            var newPrice = 150m;
            var newCategoryId = CreateValidCategoryId();
            var newBrandId = CreateValidBrandId();
            var newStockQuantity = 20;
            var newIsActive = false;

            // Act
            var result = initialProduct.UpdateDetails(newName, newDescription, newPrice, newCategoryId, newBrandId, newStockQuantity, newIsActive);

            // Assert
            result.IsError.Should().BeFalse();
            var updatedProduct = result.Value;
            updatedProduct.Name.Should().Be(newName);
            updatedProduct.Description.Should().Be(newDescription);
            updatedProduct.Price.Should().Be(newPrice);
            updatedProduct.CategoryId.Should().Be(newCategoryId);
            updatedProduct.BrandId.Should().Be(newBrandId);
            updatedProduct.StockQuantity.Should().Be(newStockQuantity);
            updatedProduct.IsActive.Should().Be(newIsActive);
        }

        [Theory]
        [InlineData("", 100, 10, "Name cannot be empty.")]
        [InlineData("Name", -1, 10, "Price cannot be negative.")]
        [InlineData("Name", 100, -1, "StockQuantity cannot be negative.")]
        public void UpdateDetails_ShouldReturnError_WhenInvalidParametersAreProvided(
            string name,
            decimal price,
            int stockQuantity,
            string expectedErrorMessage)
        {
            // Arrange
            var id = CreateValidProductId();
            var initialProduct = Product.Create(id, "Initial Name", "Initial Description", 100m, CreateValidCategoryId(), CreateValidBrandId(), 10, true).Value;

            // Act
            var result = initialProduct.UpdateDetails(name, "Updated Description", price, CreateValidCategoryId(), CreateValidBrandId(), stockQuantity, false);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Description.Contains(expectedErrorMessage));
        }

        [Fact]
        public void ToggleActiveStatus_ShouldSwitchStatusBetweenActiveAndInactive()
        {
            // Arrange
            var id = CreateValidProductId();
            var product = Product.Create(id, "Product Name", "Product Description", 100m, CreateValidCategoryId(), CreateValidBrandId(), 10, true).Value;

            // Act
            product.ToggleActiveStatus();

            // Assert
            product.IsActive.Should().BeFalse();

            // Act
            product.ToggleActiveStatus();

            // Assert
            product.IsActive.Should().BeTrue();
        }

        [Fact]
        public void GetSummary_ShouldReturnCorrectSummary()
        {
            // Arrange
            var id = CreateValidProductId();
            var name = "Product Name";
            var description = "Product Description";
            var price = 100m;
            var categoryId = CreateValidCategoryId();
            var brandId = CreateValidBrandId();
            var stockQuantity = 10;
            var isActive = true;
            var product = Product.Create(id, name, description, price, categoryId, brandId, stockQuantity, isActive).Value;

            var expectedSummary = $"Product: {name}, Price: {price}, Stock: {stockQuantity}, Active: {isActive}";

            // Act
            var summary = product.GetSummary();

            // Assert
            summary.Should().Be(expectedSummary);
        }

        [Fact]
        public void Equality_ShouldReturnTrue_ForEqualProducts()
        {
            // Arrange
            var id = CreateValidProductId();
            var product1 = Product.Create(id, "Product Name", "Product Description", 100m, CreateValidCategoryId(), CreateValidBrandId(), 10, true).Value;
            var product2 = Product.Create(id, "Product Name", "Product Description", 100m, CreateValidCategoryId(), CreateValidBrandId(), 10, true).Value;

            // Act & Assert
            product1.Should().Be(product2);
        }

        [Fact]
        public void Equality_ShouldReturnFalse_ForDifferentProducts()
        {
            // Arrange
            var id1 = CreateValidProductId();
            var id2 = new ProductId(2); // Different ID
            var product1 = Product.Create(id1, "Product Name", "Product Description", 100m, CreateValidCategoryId(), CreateValidBrandId(), 10, true).Value;
            var product2 = Product.Create(id2, "Different Name", "Different Description", 150m, CreateValidCategoryId(), CreateValidBrandId(), 20, false).Value;

            // Act & Assert
            product1.Should().NotBe(product2);
        }

        [Fact]
        public void GetHashCode_ShouldBeSame_ForEqualProducts()
        {
            // Arrange
            var id = CreateValidProductId();
            var product1 = Product.Create(id, "Product Name", "Product Description", 100m, CreateValidCategoryId(), CreateValidBrandId(), 10, true).Value;
            var product2 = Product.Create(id, "Product Name", "Product Description", 100m, CreateValidCategoryId(), CreateValidBrandId(), 10, true).Value;

            // Act & Assert
            product1.GetHashCode().Should().Be(product2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ShouldBeDifferent_ForDifferentProducts()
        {
            // Arrange
            var id1 = CreateValidProductId();
            var id2 = new ProductId(2); // Different ID
            var product1 = Product.Create(id1, "Product Name", "Product Description", 100m, CreateValidCategoryId(), CreateValidBrandId(), 10, true).Value;
            var product2 = Product.Create(id2, "Different Name", "Different Description", 150m, CreateValidCategoryId(), CreateValidBrandId(), 20, false).Value;

            // Act & Assert
            product1.GetHashCode().Should().NotBe(product2.GetHashCode());
        }
    }
}
