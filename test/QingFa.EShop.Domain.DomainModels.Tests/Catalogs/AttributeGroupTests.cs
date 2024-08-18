using FluentAssertions;

using QingFa.EShop.Domain.Catalogs.Attributes;

namespace QingFa.EShop.Domain.Tests.Catalogs
{
    public class AttributeGroupTests
    {
        private static AttributeGroupId CreateValidAttributeGroupId() => new AttributeGroupId(1); 
        [Fact]
        public void Create_ShouldReturnAttributeGroup_WhenValidParametersAreProvided()
        {
            // Arrange
            var id = CreateValidAttributeGroupId();
            var groupName = "Electronics";

            // Act
            var result = AttributeGroup.Create(id, groupName);

            // Assert
            result.IsError.Should().BeFalse();
            var attributeGroup = result.Value;
            attributeGroup.Should().NotBeNull();
            attributeGroup.GroupName.Should().Be(groupName);
            attributeGroup.Id.Should().Be(id);
        }

        [Theory]
        [InlineData("", "GroupName cannot be empty.")]
        public void Create_ShouldReturnError_WhenInvalidParametersAreProvided(
            string groupName,
            string expectedErrorMessage)
        {
            // Arrange
            var id = CreateValidAttributeGroupId();

            // Act
            var result = AttributeGroup.Create(id, groupName);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Description.Contains(expectedErrorMessage));
        }

        [Fact]
        public void UpdateDetails_ShouldUpdateAttributeGroup_WhenValidParametersAreProvided()
        {
            // Arrange
            var id = CreateValidAttributeGroupId();
            var initialAttributeGroup = AttributeGroup.Create(id, "Initial Name").Value;

            var newGroupName = "Updated Name";

            // Act
            initialAttributeGroup.UpdateDetails(newGroupName);

            // Assert
            initialAttributeGroup.GroupName.Should().Be(newGroupName);
        }

        [Theory]
        [InlineData("", "GroupName cannot be empty.")]
        public void UpdateDetails_ShouldThrowArgumentException_WhenInvalidParametersAreProvided(
            string groupName,
            string expectedErrorMessage)
        {
            // Arrange
            var id = CreateValidAttributeGroupId();
            var initialAttributeGroup = AttributeGroup.Create(id, "Initial Name").Value;

            // Act
            var exception = Assert.Throws<ArgumentException>(() => initialAttributeGroup.UpdateDetails(groupName));

            // Assert
            exception.Message.Should().Contain(expectedErrorMessage);
        }

        [Fact]
        public void Equality_ShouldReturnTrue_ForEqualAttributeGroups()
        {
            // Arrange
            var id = CreateValidAttributeGroupId();
            var groupName = "Electronics";
            var attributeGroup1 = AttributeGroup.Create(id, groupName).Value;
            var attributeGroup2 = AttributeGroup.Create(id, groupName).Value;

            // Act & Assert
            attributeGroup1.Should().Be(attributeGroup2);
        }

        [Fact]
        public void Equality_ShouldReturnFalse_ForDifferentAttributeGroups()
        {
            // Arrange
            var id = CreateValidAttributeGroupId();
            var attributeGroup1 = AttributeGroup.Create(id, "Electronics").Value;
            var attributeGroup2 = AttributeGroup.Create(id, "Home Appliances").Value;

            // Act & Assert
            attributeGroup1.Should().NotBe(attributeGroup2);
        }

        [Fact]
        public void GetHashCode_ShouldBeSame_ForEqualAttributeGroups()
        {
            // Arrange
            var id = CreateValidAttributeGroupId();
            var groupName = "Electronics";
            var attributeGroup1 = AttributeGroup.Create(id, groupName).Value;
            var attributeGroup2 = AttributeGroup.Create(id, groupName).Value;

            // Act & Assert
            attributeGroup1.GetHashCode().Should().Be(attributeGroup2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ShouldBeDifferent_ForDifferentAttributeGroups()
        {
            // Arrange
            var id = CreateValidAttributeGroupId();
            var attributeGroup1 = AttributeGroup.Create(id, "Electronics").Value;
            var attributeGroup2 = AttributeGroup.Create(id, "Home Appliances").Value;

            // Act & Assert
            attributeGroup1.GetHashCode().Should().NotBe(attributeGroup2.GetHashCode());
        }
    }
}
