using FluentAssertions;
using QingFa.EShop.Domain.Commons.ValueObjects;
using QingFa.EShop.Domain.Catalogs.Brands;

namespace QingFa.EShop.Domain.Tests.Catalogs
{
    public class BrandTests
    {
        private static BrandId CreateValidBrandId() => new BrandId(1);

        private static SeoInfo CreateValidSeoInfo() => SeoInfo.Create("Meta Title", "Meta Description", "url-slug", "https://canonical.url");

        [Fact]
        public void Create_ShouldReturnBrand_WhenValidParametersAreProvided()
        {
            // Arrange
            var id = CreateValidBrandId();
            var name = "Brand Name";
            var description = "Brand Description";
            var logoUrl = "https://logo.url";
            var seo = CreateValidSeoInfo();
            var status = BrandStatus.Active;

            // Act
            var result = Brand.Create(id, name, description, logoUrl, seo, status);

            // Assert
            result.IsError.Should().BeFalse();
            var brand = result.Value;
            brand.Should().NotBeNull();
            brand.Name.Should().Be(name);
            brand.Description.Should().Be(description);
            brand.LogoUrl.Should().Be(logoUrl);
            brand.Seo.Should().Be(seo);
            brand.Status.Should().Be(status);
        }

        [Theory]
        [InlineData("", "Description", "https://logo.url", "Name cannot be empty.")]
        [InlineData("Name", "", "https://logo.url", "Description cannot be empty.")]
        [InlineData("Name", "Description", "", "LogoUrl cannot be empty.")]
        public void Create_ShouldReturnError_WhenInvalidParametersAreProvided(
            string name,
            string description,
            string logoUrl,
            string expectedErrorMessage)
        {
            // Arrange
            var id = CreateValidBrandId();
            SeoInfo? seo = !string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(description) && !string.IsNullOrWhiteSpace(logoUrl) ? CreateValidSeoInfo() : null;
            var status = BrandStatus.Active;

            // Act
            var result = Brand.Create(id, name, description, logoUrl, seo, status);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Description.Contains(expectedErrorMessage));
        }

        [Fact]
        public void UpdateDetails_ShouldReturnUpdatedBrand_WhenValidParametersAreProvided()
        {
            // Arrange
            var id = CreateValidBrandId();
            var initialBrand = Brand.Create(id, "Initial Name", "Initial Description", "https://logo.url", CreateValidSeoInfo(), BrandStatus.Active).Value;

            var newName = "Updated Name";
            var newDescription = "Updated Description";
            var newLogoUrl = "https://newlogo.url";
            var newSeo = CreateValidSeoInfo();
            var newStatus = BrandStatus.Inactive;

            // Act
            var result = initialBrand.UpdateDetails(newName, newDescription, newLogoUrl, newSeo, newStatus);

            // Assert
            result.IsError.Should().BeFalse();
            var updatedBrand = result.Value;
            updatedBrand.Name.Should().Be(newName);
            updatedBrand.Description.Should().Be(newDescription);
            updatedBrand.LogoUrl.Should().Be(newLogoUrl);
            updatedBrand.Seo.Should().Be(newSeo);
            updatedBrand.Status.Should().Be(newStatus);
        }

        [Theory]
        [InlineData("", "Description", "https://logo.url", "Name cannot be empty.")]
        [InlineData("Name", "", "https://logo.url", "Description cannot be empty.")]
        [InlineData("Name", "Description", "", "LogoUrl cannot be empty.")]
        public void UpdateDetails_ShouldReturnError_WhenInvalidParametersAreProvided(
            string name,
            string description,
            string logoUrl,
            string expectedErrorMessage)
        {
            // Arrange
            var id = CreateValidBrandId();
            var initialBrand = Brand.Create(id, "Initial Name", "Initial Description", "https://logo.url", CreateValidSeoInfo(), BrandStatus.Active).Value;

            // Act
            var result = initialBrand.UpdateDetails(name, description, logoUrl,
                string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(logoUrl) ? null : CreateValidSeoInfo(),
                BrandStatus.Active);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Description.Contains(expectedErrorMessage));
        }

        [Fact]
        public void GetSummary_ShouldReturnCorrectSummary()
        {
            // Arrange
            var id = CreateValidBrandId();
            var name = "Brand Name";
            var description = "Brand Description";
            var logoUrl = "https://logo.url";
            var seo = CreateValidSeoInfo();
            var status = BrandStatus.Active;
            var brand = Brand.Create(id, name, description, logoUrl, seo, status).Value;

            var expectedSummary = $"Brand: {name}, Description: {description}, LogoUrl: {logoUrl}, Seo: {seo.MetaTitle}, Status: {status}";

            // Act
            var summary = brand.GetSummary();

            // Assert
            summary.Should().Be(expectedSummary);
        }

        [Fact]
        public void Equality_ShouldReturnTrue_ForEqualBrands()
        {
            // Arrange
            var id = CreateValidBrandId();
            var seo = CreateValidSeoInfo();
            var status = BrandStatus.Active;
            var brand1 = Brand.Create(id, "Brand Name", "Brand Description", "https://logo.url", seo, status).Value;
            var brand2 = Brand.Create(id, "Brand Name", "Brand Description", "https://logo.url", seo, status).Value;

            // Act & Assert
            brand1.Should().Be(brand2);
        }

        [Fact]
        public void Equality_ShouldReturnFalse_ForDifferentBrands()
        {
            // Arrange
            var brand1 = Brand.Create(CreateValidBrandId(), "Brand Name", "Brand Description", "https://logo.url", CreateValidSeoInfo(), BrandStatus.Active).Value;
            var brand2 = Brand.Create(CreateValidBrandId(), "Different Name", "Different Description", "https://logo.url", CreateValidSeoInfo(), BrandStatus.Inactive).Value;

            // Act & Assert
            brand1.Should().NotBe(brand2);
        }

        [Fact]
        public void GetHashCode_ShouldBeSame_ForEqualBrands()
        {
            // Arrange
            var id = CreateValidBrandId();
            var seo = CreateValidSeoInfo();
            var status = BrandStatus.Active;
            var brand1 = Brand.Create(id, "Brand Name", "Brand Description", "https://logo.url", seo, status).Value;
            var brand2 = Brand.Create(id, "Brand Name", "Brand Description", "https://logo.url", seo, status).Value;

            // Act & Assert
            brand1.GetHashCode().Should().Be(brand2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ShouldBeDifferent_ForDifferentBrands()
        {
            // Arrange
            var brand1 = Brand.Create(CreateValidBrandId(), "Brand Name", "Brand Description", "https://logo.url", CreateValidSeoInfo(), BrandStatus.Active).Value;
            var brand2 = Brand.Create(CreateValidBrandId(), "Different Name", "Different Description", "https://logo.url", CreateValidSeoInfo(), BrandStatus.Inactive).Value;

            // Act & Assert
            brand1.GetHashCode().Should().NotBe(brand2.GetHashCode());
        }
    }
}
