using FluentAssertions;
using Xunit;
using ErrorOr;
using QingFa.EShop.Domain.Catalogs.Attributes;
using QingFa.EShop.Domain.Catalogs.Variants;
using QingFa.EShop.Domain.DomainModels.Errors;

namespace QingFa.EShop.Domain.Tests.Catalogs.Variants
{
    public class ProductVariantAttributeTests
    {
        private readonly ProductVariantAttributeId _validProductVariantAttributeId = new ProductVariantAttributeId(1);
        private readonly ProductVariantId _validProductVariantId = new ProductVariantId(1);
        private readonly ProductAttributeId _validProductAttributeId = new ProductAttributeId(1);
        private readonly AttributeOptionId _validAttributeOptionId = new AttributeOptionId(1);

        [Fact]
        public void Create_ShouldReturnError_WhenProductVariantIdIsNull()
        {
            // Act
            var result = ProductVariantAttribute.Create(
                _validProductVariantAttributeId,
                null,
                _validProductAttributeId,
                _validAttributeOptionId,
                "Custom Value",
                true,
                true
            );

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Code.Should().Be(CoreErrors.NullArgument(nameof(ProductVariantId)).Code);
        }

        [Fact]
        public void Create_ShouldReturnError_WhenAttributeIdIsNull()
        {
            // Act
            var result = ProductVariantAttribute.Create(
                _validProductVariantAttributeId,
                _validProductVariantId,
                null,
                _validAttributeOptionId,
                "Custom Value",
                true,
                true
            );

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Code.Should().Be(CoreErrors.NullArgument(nameof(ProductAttributeId)).Code);
        }

        [Fact]
        public void Create_ShouldReturnError_WhenBothAttributeOptionIdAndCustomValueAreNull()
        {
            // Act
            var result = ProductVariantAttribute.Create(
                _validProductVariantAttributeId,
                _validProductVariantId,
                _validProductAttributeId,
                null,
                null,
                true,
                true
            );

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Code.Should().Be(CoreErrors.ValidationError(nameof(AttributeOptionId), "Either AttributeOptionId or CustomValue must be provided.").Code);
        }

        [Fact]
        public void Create_ShouldReturnProductVariantAttribute_WhenValidInputsAreProvided()
        {
            // Act
            var result = ProductVariantAttribute.Create(
                _validProductVariantAttributeId,
                _validProductVariantId,
                _validProductAttributeId,
                _validAttributeOptionId,
                "Custom Value",
                true,
                true
            );

            // Assert
            result.IsError.Should().BeFalse();
            var productVariantAttribute = result.Value;
            productVariantAttribute.ProductVariantId.Should().Be(_validProductVariantId);
            productVariantAttribute.AttributeId.Should().Be(_validProductAttributeId);
            productVariantAttribute.AttributeOptionId.Should().Be(_validAttributeOptionId);
            productVariantAttribute.CustomValue.Should().Be("Custom Value");
            productVariantAttribute.IsRequired.Should().BeTrue();
            productVariantAttribute.IsVisibleToCustomer.Should().BeTrue();
        }

        [Fact]
        public void UpdateDetails_ShouldReturnError_WhenAttributeIdIsNull()
        {
            // Arrange
            var productVariantAttribute = ProductVariantAttribute.Create(
                _validProductVariantAttributeId,
                _validProductVariantId,
                _validProductAttributeId,
                _validAttributeOptionId,
                "Custom Value",
                true,
                true
            ).Value;

            // Act
            var result = productVariantAttribute.UpdateDetails(
                null,
                _validAttributeOptionId,
                "Updated Custom Value"
            );

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Code.Should().Be(CoreErrors.NullArgument(nameof(ProductAttributeId)).Code);
        }

        [Fact]
        public void UpdateDetails_ShouldReturnError_WhenBothAttributeOptionIdAndCustomValueAreNull()
        {
            // Arrange
            var productVariantAttribute = ProductVariantAttribute.Create(
                _validProductVariantAttributeId,
                _validProductVariantId,
                _validProductAttributeId,
                _validAttributeOptionId,
                "Custom Value",
                true,
                true
            ).Value;

            // Act
            var result = productVariantAttribute.UpdateDetails(
                _validProductAttributeId,
                null,
                null
            );

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Code.Should().Be(CoreErrors.ValidationError(nameof(AttributeOptionId), "Either AttributeOptionId or CustomValue must be provided.").Code);
        }

        [Fact]
        public void UpdateDetails_ShouldUpdateAttributes_WhenValidInputsAreProvided()
        {
            // Arrange
            var productVariantAttribute = ProductVariantAttribute.Create(
                _validProductVariantAttributeId,
                _validProductVariantId,
                _validProductAttributeId,
                _validAttributeOptionId,
                "Custom Value",
                true,
                true
            ).Value;

            // Act
            var result = productVariantAttribute.UpdateDetails(
                _validProductAttributeId,
                _validAttributeOptionId,
                "Updated Custom Value"
            );

            // Assert
            result.IsError.Should().BeFalse();
            productVariantAttribute.AttributeId.Should().Be(_validProductAttributeId);
            productVariantAttribute.AttributeOptionId.Should().Be(_validAttributeOptionId);
            productVariantAttribute.CustomValue.Should().Be("Updated Custom Value");
        }
    }
}
