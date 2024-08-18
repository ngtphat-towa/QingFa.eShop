using FluentAssertions;

using QingFa.EShop.Domain.Catalogs.Attributes;

namespace QingFa.EShop.Domain.Tests.Catalogs
{
    public class AttributeOptionTests
    {
        private static AttributeOptionId CreateValidAttributeOptionId() => new AttributeOptionId(1);
        private static ProductAttributeId CreateValidProductAttributeId() => new ProductAttributeId(1);

        [Fact]
        public void Create_ShouldReturnAttributeOption_WhenValidParametersAreProvided()
        {
            // Arrange
            var id = CreateValidAttributeOptionId();
            var attributeId = CreateValidProductAttributeId();
            var optionValue = "Option Value";
            var description = "Description";
            var isDefault = true;
            var sortOrder = 1;

            // Act
            var result = AttributeOption.Create(id, attributeId, optionValue, description, isDefault, sortOrder);

            // Assert
            result.IsError.Should().BeFalse();
            var attributeOption = result.Value;
            attributeOption.Should().NotBeNull();
            attributeOption.OptionValue.Should().Be(optionValue);
            attributeOption.Description.Should().Be(description);
            attributeOption.IsDefault.Should().Be(isDefault);
            attributeOption.SortOrder.Should().Be(sortOrder);
            attributeOption.AttributeId.Should().Be(attributeId);
        }

        [Theory]
        [InlineData("", "Description", 1, "OptionValue cannot be empty.")]
        [InlineData("Option Value", "", 1, "Description cannot be empty.")]
        [InlineData("Option Value", "Description", -1, "SortOrder cannot be negative.")]
        public void Create_ShouldReturnError_WhenInvalidParametersAreProvided(
            string optionValue,
            string description,
            int sortOrder,
            string expectedErrorMessage)
        {
            // Arrange
            var id = CreateValidAttributeOptionId();
            var attributeId = CreateValidProductAttributeId();

            // Act
            var result = AttributeOption.Create(id, attributeId, optionValue, description, true, sortOrder);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Description.Contains(expectedErrorMessage));
        }

        [Fact]
        public void UpdateDetails_ShouldReturnUpdatedAttributeOption_WhenValidParametersAreProvided()
        {
            // Arrange
            var id = CreateValidAttributeOptionId();
            var attributeId = CreateValidProductAttributeId();
            var initialAttributeOption = AttributeOption.Create(id, attributeId, "Initial Value", "Initial Description", false, 0).Value;

            var newOptionValue = "Updated Value";
            var newDescription = "Updated Description";
            var newIsDefault = true;
            var newSortOrder = 2;

            // Act
            var result = initialAttributeOption.UpdateDetails(newOptionValue, newDescription, newIsDefault, newSortOrder);

            // Assert
            result.IsError.Should().BeFalse();
            var updatedAttributeOption = initialAttributeOption;
            updatedAttributeOption.OptionValue.Should().Be(newOptionValue);
            updatedAttributeOption.Description.Should().Be(newDescription);
            updatedAttributeOption.IsDefault.Should().Be(newIsDefault);
            updatedAttributeOption.SortOrder.Should().Be(newSortOrder);
        }

        [Theory]
        [InlineData("", "Updated Description", 2, "OptionValue cannot be empty.")]
        [InlineData("Updated Value", "", 2, "Description cannot be empty.")]
        [InlineData("Updated Value", "Updated Description", -1, "SortOrder cannot be negative.")]
        public void UpdateDetails_ShouldReturnError_WhenInvalidParametersAreProvided(
            string optionValue,
            string description,
            int sortOrder,
            string expectedErrorMessage)
        {
            // Arrange
            var id = CreateValidAttributeOptionId();
            var attributeId = CreateValidProductAttributeId();
            var initialAttributeOption = AttributeOption.Create(id, attributeId, "Initial Value", "Initial Description", false, 0).Value;

            // Act
            var result = initialAttributeOption.UpdateDetails(optionValue, description, true, sortOrder);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Description.Contains(expectedErrorMessage));
        }

        [Fact]
        public void Equality_ShouldReturnTrue_ForEqualAttributeOptions()
        {
            // Arrange
            var id = CreateValidAttributeOptionId();
            var attributeId = CreateValidProductAttributeId();
            var attributeOption1 = AttributeOption.Create(id, attributeId, "Value", "Description", true, 1).Value;
            var attributeOption2 = AttributeOption.Create(id, attributeId, "Value", "Description", true, 1).Value;

            // Act & Assert
            attributeOption1.Should().Be(attributeOption2);
        }

        [Fact]
        public void Equality_ShouldReturnFalse_ForDifferentAttributeOptions()
        {
            // Arrange
            // Use different IDs to ensure the AttributeOption instances are different
            var id1 = new AttributeOptionId(1);
            var id2 = new AttributeOptionId(2);
            var attributeId = CreateValidProductAttributeId(); // Assuming a valid ID

            var attributeOption1 = AttributeOption.Create(id1, attributeId, "Value", "Description", true, 1).Value;
            var attributeOption2 = AttributeOption.Create(id2, attributeId, "Value", "Description", true, 1).Value;

            // Act & Assert
            attributeOption1.Should().NotBe(attributeOption2);
        }


        [Fact]
        public void GetHashCode_ShouldBeSame_ForEqualAttributeOptions()
        {
            // Arrange
            var id = CreateValidAttributeOptionId();
            var attributeId = CreateValidProductAttributeId();
            var attributeOption1 = AttributeOption.Create(id, attributeId, "Value", "Description", true, 1).Value;
            var attributeOption2 = AttributeOption.Create(id, attributeId, "Value", "Description", true, 1).Value;

            // Act & Assert
            attributeOption1.GetHashCode().Should().Be(attributeOption2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ShouldBeDifferent_ForDifferentAttributeOptions()
        {
            // Arrange
            var id1 = CreateValidAttributeOptionId();
            var id2 = CreateValidAttributeOptionId();
            var attributeId = CreateValidProductAttributeId();
            var attributeOption1 = AttributeOption.Create(id1, attributeId, "Value", "Description", true, 1).Value;
            var attributeOption2 = AttributeOption.Create(id2, attributeId, "Different Value", "Description", true, 1).Value;

            // Act & Assert
            attributeOption1.GetHashCode().Should().NotBe(attributeOption2.GetHashCode());
        }
    }
}
