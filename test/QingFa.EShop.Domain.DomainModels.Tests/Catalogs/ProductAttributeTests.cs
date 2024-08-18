using FluentAssertions;

using QingFa.EShop.Domain.Catalogs.Attributes;

namespace QingFa.EShop.Domain.Tests.Catalogs
{
    public class ProductAttributeTests
    {
        private static ProductAttributeId CreateValidProductAttributeId() => new ProductAttributeId(1);
        private static AttributeGroupId CreateValidAttributeGroupId() => new AttributeGroupId(1);
        private static AttributeType CreateValidAttributeType() => AttributeType.Text; // Assuming this is a valid type

        [Fact]
        public void Create_ShouldReturnProductAttribute_WhenValidParametersAreProvided()
        {
            // Arrange
            var id = CreateValidProductAttributeId();
            var name = "Attribute Name";
            var attributeCode = "AttrCode";
            var type = CreateValidAttributeType();
            var isRequired = true;
            var isFilterable = false;
            var showToCustomers = true;
            var sortOrder = 1;
            var attributeGroupId = CreateValidAttributeGroupId();

            // Act
            var result = ProductAttribute.Create(id, name, attributeCode, type, isRequired, isFilterable, showToCustomers, sortOrder, attributeGroupId);

            // Assert
            result.IsError.Should().BeFalse();
            var productAttribute = result.Value;
            productAttribute.Should().NotBeNull();
            productAttribute.Name.Should().Be(name);
            productAttribute.AttributeCode.Should().Be(attributeCode);
            productAttribute.Type.Should().Be(type);
            productAttribute.IsRequired.Should().Be(isRequired);
            productAttribute.IsFilterable.Should().Be(isFilterable);
            productAttribute.ShowToCustomers.Should().Be(showToCustomers);
            productAttribute.SortOrder.Should().Be(sortOrder);
            productAttribute.AttributeGroupId.Should().Be(attributeGroupId);
        }

        [Theory]
        [InlineData("", "AttrCode", 1, "Name cannot be empty.")]
        [InlineData("Name", "", 1, "AttributeCode cannot be empty.")]
        [InlineData("Name", "AttrCode", -1, "SortOrder cannot be negative.")]
        public void Create_ShouldReturnError_WhenInvalidParametersAreProvided(
            string name,
            string attributeCode,
            int sortOrder,
            string expectedErrorMessage)
        {
            // Arrange
            var id = CreateValidProductAttributeId();
            var type = CreateValidAttributeType();
            var isRequired = true;
            var isFilterable = false;
            var showToCustomers = true;
            var attributeGroupId = CreateValidAttributeGroupId();

            // Act
            var result = ProductAttribute.Create(id, name, attributeCode, type, isRequired, isFilterable, showToCustomers, sortOrder, attributeGroupId);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Description.Contains(expectedErrorMessage));
        }

        [Fact]
        public void UpdateDetails_ShouldReturnUpdatedProductAttribute_WhenValidParametersAreProvided()
        {
            // Arrange
            var id = CreateValidProductAttributeId();
            var initialProductAttribute = ProductAttribute.Create(id, "Initial Name", "Initial Code", CreateValidAttributeType(), true, false, true, 1, CreateValidAttributeGroupId()).Value;

            var newName = "Updated Name";
            var newAttributeCode = "Updated Code";
            var newType = CreateValidAttributeType();
            var newIsRequired = false;
            var newIsFilterable = true;
            var newShowToCustomers = false;
            var newSortOrder = 2;
            var newAttributeGroupId = CreateValidAttributeGroupId();

            // Act
            var result = initialProductAttribute.UpdateDetails(newName, newAttributeCode, newType, newIsRequired, newIsFilterable, newShowToCustomers, newSortOrder, newAttributeGroupId);

            // Assert
            result.IsError.Should().BeFalse();
            var updatedProductAttribute = initialProductAttribute;
            updatedProductAttribute.Name.Should().Be(newName);
            updatedProductAttribute.AttributeCode.Should().Be(newAttributeCode);
            updatedProductAttribute.Type.Should().Be(newType);
            updatedProductAttribute.IsRequired.Should().Be(newIsRequired);
            updatedProductAttribute.IsFilterable.Should().Be(newIsFilterable);
            updatedProductAttribute.ShowToCustomers.Should().Be(newShowToCustomers);
            updatedProductAttribute.SortOrder.Should().Be(newSortOrder);
            updatedProductAttribute.AttributeGroupId.Should().Be(newAttributeGroupId);
        }

        [Theory]
        [InlineData("", "Updated Code", 2, "Name cannot be empty.")]
        [InlineData("Updated Name", "", 2, "AttributeCode cannot be empty.")]
        [InlineData("Updated Name", "Updated Code", -1, "SortOrder cannot be negative.")]
        public void UpdateDetails_ShouldReturnError_WhenInvalidParametersAreProvided(
            string name,
            string attributeCode,
            int sortOrder,
            string expectedErrorMessage)
        {
            // Arrange
            var id = CreateValidProductAttributeId();
            var initialProductAttribute = ProductAttribute.Create(id, "Initial Name", "Initial Code", CreateValidAttributeType(), true, false, true, 1, CreateValidAttributeGroupId()).Value;

            // Act
            var result = initialProductAttribute.UpdateDetails(name, attributeCode, CreateValidAttributeType(), true, false, true, sortOrder, CreateValidAttributeGroupId());

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Description.Contains(expectedErrorMessage));
        }

        [Fact]
        public void Equality_ShouldReturnTrue_ForEqualProductAttributes()
        {
            // Arrange
            var id = CreateValidProductAttributeId();
            var attributeCode = "AttrCode";
            var productAttribute1 = ProductAttribute.Create(id, "Name", attributeCode, CreateValidAttributeType(), true, false, true, 1, CreateValidAttributeGroupId()).Value;
            var productAttribute2 = ProductAttribute.Create(id, "Name", attributeCode, CreateValidAttributeType(), true, false, true, 1, CreateValidAttributeGroupId()).Value;

            // Act & Assert
            productAttribute1.Should().Be(productAttribute2);
        }

        [Fact]
        public void Equality_ShouldReturnFalse_ForDifferentProductAttributes()
        {
            // Arrange
            var id1 = CreateValidProductAttributeId();
            var id2 = CreateValidProductAttributeId();
            var attributeCode = "AttrCode";
            var productAttribute1 = ProductAttribute.Create(id1, "Name", attributeCode, CreateValidAttributeType(), true, false, true, 1, CreateValidAttributeGroupId()).Value;
            var productAttribute2 = ProductAttribute.Create(id2, "Different Name", attributeCode, CreateValidAttributeType(), true, false, true, 1, CreateValidAttributeGroupId()).Value;

            // Act & Assert
            productAttribute1.Should().NotBe(productAttribute2);
        }

        [Fact]
        public void GetHashCode_ShouldBeSame_ForEqualProductAttributes()
        {
            // Arrange
            var id = CreateValidProductAttributeId();
            var attributeCode = "AttrCode";
            var productAttribute1 = ProductAttribute.Create(id, "Name", attributeCode, CreateValidAttributeType(), true, false, true, 1, CreateValidAttributeGroupId()).Value;
            var productAttribute2 = ProductAttribute.Create(id, "Name", attributeCode, CreateValidAttributeType(), true, false, true, 1, CreateValidAttributeGroupId()).Value;

            // Act & Assert
            productAttribute1.GetHashCode().Should().Be(productAttribute2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ShouldBeDifferent_ForDifferentProductAttributes()
        {
            // Arrange
            var id1 = CreateValidProductAttributeId();
            var id2 = CreateValidProductAttributeId();
            var attributeCode = "AttrCode";
            var productAttribute1 = ProductAttribute.Create(id1, "Name", attributeCode, CreateValidAttributeType(), true, false, true, 1, CreateValidAttributeGroupId()).Value;
            var productAttribute2 = ProductAttribute.Create(id2, "Different Name", attributeCode, CreateValidAttributeType(), true, false, true, 1, CreateValidAttributeGroupId()).Value;

            // Act & Assert
            productAttribute1.GetHashCode().Should().NotBe(productAttribute2.GetHashCode());
        }
    }
}
