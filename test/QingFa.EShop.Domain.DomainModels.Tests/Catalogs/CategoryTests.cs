using FluentAssertions;
using QingFa.EShop.Domain.Commons.ValueObjects;
using QingFa.EShop.Domain.Catalogs.Categories;

namespace QingFa.EShop.Domain.Tests.Catalogs
{
    public class CategoryTests
    {
        private static CategoryId CreateValidCategoryId() => new CategoryId(1);

        private static SeoInfo CreateValidSeoInfo() => SeoInfo.Create("Meta Title", "Meta Description", "url-slug", "https://canonical.url");

        [Fact]
        public void Create_ShouldReturnCategory_WhenValidParametersAreProvided()
        {
            // Arrange
            var id = CreateValidCategoryId();
            var name = "Category Name";
            var description = "Category Description";
            var bannerURL = "https://banner.url";
            var seo = CreateValidSeoInfo();
            var status = CategoryStatus.Active;
            var includeToStore = true;
            var parentId = (CategoryId?)null;

            // Act
            var result = Category.Create(id, name, description, bannerURL, status, includeToStore, parentId, seo);

            // Assert
            result.IsError.Should().BeFalse();
            var category = result.Value;
            category.Should().NotBeNull();
            category.Name.Should().Be(name);
            category.Description.Should().Be(description);
            category.BannerURL.Should().Be(bannerURL);
            category.Seo.Should().Be(seo);
            category.Status.Should().Be(status);
            category.IncludeToStore.Should().Be(includeToStore);
            category.ParentId.Should().Be(parentId);
        }

        [Theory]
        [InlineData("", "Description", "https://banner.url", "Name cannot be empty.")]
        [InlineData("Name", "", "https://banner.url", "Description cannot be empty.")]
        [InlineData("Name", "Description", "", "BannerURL cannot be empty.")]
        public void Create_ShouldReturnError_WhenInvalidParametersAreProvided(
            string name,
            string description,
            string bannerURL,
            string expectedErrorMessage)
        {
            // Arrange
            SeoInfo? seo = !string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(description) && !string.IsNullOrWhiteSpace(bannerURL)
                ? CreateValidSeoInfo()
                : null;

            var id = CreateValidCategoryId();
            var status = CategoryStatus.Active;
            var includeToStore = true;
            var parentId = (CategoryId?)null;

            // Act
            var result = Category.Create(id, name, description, bannerURL, status, includeToStore, parentId, seo);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Description.Contains(expectedErrorMessage));
        }

        [Fact]
        public void UpdateDetails_ShouldReturnUpdatedCategory_WhenValidParametersAreProvided()
        {
            // Arrange
            var id = CreateValidCategoryId();
            var initialCategory = Category.Create(id, "Initial Name", "Initial Description", "https://banner.url", CategoryStatus.Active, true, null, CreateValidSeoInfo()).Value;

            var newName = "Updated Name";
            var newDescription = "Updated Description";
            var newBannerURL = "https://newbanner.url";
            var newSeo = CreateValidSeoInfo();
            var newStatus = CategoryStatus.Inactive;

            // Act
            var result = initialCategory.UpdateDetails(newName, newDescription, newBannerURL, newStatus, newSeo);

            // Assert
            result.IsError.Should().BeFalse();
            var updatedCategory = result.Value;
            updatedCategory.Name.Should().Be(newName);
            updatedCategory.Description.Should().Be(newDescription);
            updatedCategory.BannerURL.Should().Be(newBannerURL);
            updatedCategory.Seo.Should().Be(newSeo);
            updatedCategory.Status.Should().Be(newStatus);
        }

        [Theory]
        [InlineData("", "Description", "https://banner.url", "Name cannot be empty.")]
        [InlineData("Name", "", "https://banner.url", "Description cannot be empty.")]
        [InlineData("Name", "Description", "", "BannerURL cannot be empty.")]
        public void UpdateDetails_ShouldReturnError_WhenInvalidParametersAreProvided(
            string name,
            string description,
            string bannerURL,
            string expectedErrorMessage)
        {
            // Arrange
            var id = CreateValidCategoryId();
            var initialCategory = Category.Create(id, "Initial Name", "Initial Description", "https://banner.url", CategoryStatus.Active, true, null, CreateValidSeoInfo()).Value;

            SeoInfo? newSeo = !string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(description) && !string.IsNullOrWhiteSpace(bannerURL)
                ? CreateValidSeoInfo()
                : null;

            // Act
            var result = initialCategory.UpdateDetails(name, description, bannerURL, CategoryStatus.Active, newSeo!);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Description.Contains(expectedErrorMessage));
        }

        [Fact]
        public void ToggleActiveStatus_ShouldSwitchStatusBetweenActiveAndInactive()
        {
            // Arrange
            var id = CreateValidCategoryId();
            var category = Category.Create(id, "Category Name", "Category Description", "https://banner.url", CategoryStatus.Active, true, null, CreateValidSeoInfo()).Value;

            // Act
            category.ToggleActiveStatus();

            // Assert
            category.Status.Should().Be(CategoryStatus.Inactive);

            // Act
            category.ToggleActiveStatus();

            // Assert
            category.Status.Should().Be(CategoryStatus.Active);
        }

        [Fact]
        public void UpdateParent_ShouldReturnUpdatedCategory_WhenValidParentIdIsProvided()
        {
            // Arrange
            var id = CreateValidCategoryId();
            var newParentId = CreateValidCategoryId();
            var initialCategory = Category.Create(id, "Category Name", "Category Description", "https://banner.url", CategoryStatus.Active, true, null, CreateValidSeoInfo()).Value;

            // Act
            var result = initialCategory.UpdateParent(newParentId);

            // Assert
            result.IsError.Should().BeFalse();
            var updatedCategory = result.Value;
            updatedCategory.ParentId.Should().Be(newParentId);
        }

        [Fact]
        public void UpdateParent_ShouldReturnError_WhenNewParentIdIsSameAsCurrent()
        {
            // Arrange
            var id = CreateValidCategoryId();
            var initialCategory = Category.Create(id, "Category Name", "Category Description", "https://banner.url", CategoryStatus.Active, true, id, CreateValidSeoInfo()).Value;

            // Act
            var result = initialCategory.UpdateParent(id);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Description.Contains("New ParentId must be different from the current one."));
        }

        [Fact]
        public void GetSummary_ShouldReturnCorrectSummary()
        {
            // Arrange
            var id = CreateValidCategoryId();
            var name = "Category Name";
            var description = "Category Description";
            var bannerURL = "https://banner.url";
            var seo = CreateValidSeoInfo();
            var status = CategoryStatus.Active;
            var includeToStore = true;
            var parentId = (CategoryId?)null;
            var category = Category.Create(id, name, description, bannerURL, status, includeToStore, parentId, seo).Value;

            var expectedSummary = $"Category: {name}, Status: {status}, ParentId: {parentId}, Seo: {seo.MetaTitle}";

            // Act
            var summary = category.GetSummary();

            // Assert
            summary.Should().Be(expectedSummary);
        }

        [Fact]
        public void Equality_ShouldReturnTrue_ForEqualCategories()
        {
            // Arrange
            var id = CreateValidCategoryId();
            var seo = CreateValidSeoInfo();
            var status = CategoryStatus.Active;
            var category1 = Category.Create(id, "Category Name", "Category Description", "https://banner.url", status, true, null, seo).Value;
            var category2 = Category.Create(id, "Category Name", "Category Description", "https://banner.url", status, true, null, seo).Value;

            // Act & Assert
            category1.Should().Be(category2);
        }

        [Fact]
        public void Equality_ShouldReturnFalse_ForDifferentCategories()
        {
            // Arrange
            var id1 = CreateValidCategoryId();
            var id2 = new CategoryId(2); // Different ID
            var category1 = Category.Create(id1, "Category Name", "Category Description", "https://banner.url", CategoryStatus.Active, true, null, CreateValidSeoInfo()).Value;
            var category2 = Category.Create(id2, "Different Name", "Different Description", "https://banner.url", CategoryStatus.Inactive, false, id1, CreateValidSeoInfo()).Value;

            // Act & Assert
            category1.Should().NotBe(category2);
        }

        [Fact]
        public void GetHashCode_ShouldBeSame_ForEqualCategories()
        {
            // Arrange
            var id = CreateValidCategoryId();
            var seo = CreateValidSeoInfo();
            var status = CategoryStatus.Active;
            var category1 = Category.Create(id, "Category Name", "Category Description", "https://banner.url", status, true, null, seo).Value;
            var category2 = Category.Create(id, "Category Name", "Category Description", "https://banner.url", status, true, null, seo).Value;

            // Act & Assert
            category1.GetHashCode().Should().Be(category2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ShouldBeDifferent_ForDifferentCategories()
        {
            // Arrange
            var id1 = CreateValidCategoryId();
            var id2 = new CategoryId(2); // Different ID
            var category1 = Category.Create(id1, "Category Name", "Category Description", "https://banner.url", CategoryStatus.Active, true, null, CreateValidSeoInfo()).Value;
            var category2 = Category.Create(id2, "Different Name", "Different Description", "https://banner.url", CategoryStatus.Inactive, false, id1, CreateValidSeoInfo()).Value;

            // Act & Assert
            category1.GetHashCode().Should().NotBe(category2.GetHashCode());
        }
    }
}
