using FluentAssertions;

using QingFa.EShop.Domain.Commons.ValueObjects;

using System.Reflection;

namespace QingFa.EShop.Domain.Tests.Commons
{
    public class SeoInfoTests
    {
        [Fact]
        public void Create_ShouldThrowArgumentException_WhenMetaTitleIsNullOrWhiteSpace()
        {
            // Arrange & Act
            Action act = () => SeoInfo.Create(null!, "Description", "url-slug", "https://canonical.url");

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("MetaTitle cannot be null or whitespace. (Parameter 'metaTitle')");
        }

        [Fact]
        public void Create_ShouldThrowArgumentException_WhenURLSlugIsNullOrWhiteSpace()
        {
            // Arrange & Act
            Action act = () => SeoInfo.Create("Title", "Description", null!, "https://canonical.url");

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("URLSlug cannot be null or whitespace. (Parameter 'urlSlug')");
        }

        [Fact]
        public void Create_ShouldCreateInstance_WhenAllParametersAreValid()
        {
            // Arrange & Act
            var seoInfo = SeoInfo.Create("MetaTitle", "MetaDescription", "url-slug", "https://canonical.url");

            // Assert
            seoInfo.Should().NotBeNull();
            seoInfo.MetaTitle.Should().Be("MetaTitle");
            seoInfo.MetaDescription.Should().Be("MetaDescription");
            seoInfo.URLSlug.Should().Be("url-slug");
            seoInfo.CanonicalURL.Should().Be("https://canonical.url");
        }

        [Fact]
        public void CreateDefault_ShouldCreateDefaultInstance()
        {
            // Arrange & Act
            var seoInfo = SeoInfo.CreateDefault();

            // Assert
            seoInfo.Should().NotBeNull();
            seoInfo.MetaTitle.Should().Be("Default Title");
            seoInfo.MetaDescription.Should().Be("Default Description");
            seoInfo.URLSlug.Should().Be("default-url-slug");
            seoInfo.CanonicalURL.Should().Be("https://default-url.com");
        }

        [Fact]
        public void CreateForHomepage_ShouldCreateHomepageInstance()
        {
            // Arrange
            var metaTitle = "Homepage Title";

            // Act
            var seoInfo = SeoInfo.CreateForHomepage(metaTitle);

            // Assert
            seoInfo.Should().NotBeNull();
            seoInfo.MetaTitle.Should().Be(metaTitle);
            seoInfo.MetaDescription.Should().Be("Homepage description.");
            seoInfo.URLSlug.Should().Be("homepage");
            seoInfo.CanonicalURL.Should().Be("https://example.com/homepage");
        }

        [Fact]
        public void Equality_ShouldBeBasedOnAtomicValues()
        {
            // Arrange
            var seoInfo1 = SeoInfo.Create("MetaTitle", "MetaDescription", "url-slug", "https://canonical.url");
            var seoInfo2 = SeoInfo.Create("MetaTitle", "MetaDescription", "url-slug", "https://canonical.url");

            // Act & Assert
            seoInfo1.Should().Be(seoInfo2);
        }

        [Fact]
        public void Equality_ShouldNotBeEqualWhenPropertiesDiffer()
        {
            // Arrange
            var seoInfo1 = SeoInfo.Create("MetaTitle1", "MetaDescription1", "url-slug1", "https://canonical.url1");
            var seoInfo2 = SeoInfo.Create("MetaTitle2", "MetaDescription2", "url-slug2", "https://canonical.url2");

            // Act & Assert
            seoInfo1.Should().NotBe(seoInfo2);
        }

        [Fact]
        public void GetHashCode_ShouldReturnSameHashCode_ForEqualInstances()
        {
            // Arrange
            var seoInfo1 = SeoInfo.Create("MetaTitle", "MetaDescription", "url-slug", "https://canonical.url");
            var seoInfo2 = SeoInfo.Create("MetaTitle", "MetaDescription", "url-slug", "https://canonical.url");

            // Act & Assert
            seoInfo1.GetHashCode().Should().Be(seoInfo2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ShouldReturnDifferentHashCode_WhenPropertiesDiffer()
        {
            // Arrange
            var seoInfo1 = SeoInfo.Create("MetaTitle1", "MetaDescription1", "url-slug1", "https://canonical.url1");
            var seoInfo2 = SeoInfo.Create("MetaTitle2", "MetaDescription2", "url-slug2", "https://canonical.url2");

            // Act & Assert
            seoInfo1.GetHashCode().Should().NotBe(seoInfo2.GetHashCode());
        }

        [Fact]
        public void GetAtomicValues_ShouldReturnCorrectValues()
        {
            // Arrange
            var seoInfo = SeoInfo.Create("MetaTitle", "MetaDescription", "url-slug", "https://canonical.url");

            // Use reflection to access the protected method
            var methodInfo = typeof(SeoInfo).GetMethod("GetAtomicValues", BindingFlags.NonPublic | BindingFlags.Instance);
            var atomicValues = (IEnumerable<object>)methodInfo!.Invoke(seoInfo, null)!;

            var atomicValuesArray = atomicValues.ToArray();

            // Assert
            atomicValuesArray.Should().HaveCount(4);
            atomicValuesArray[0].Should().Be("MetaTitle");
            atomicValuesArray[1].Should().Be("MetaDescription");
            atomicValuesArray[2].Should().Be("url-slug");
            atomicValuesArray[3].Should().Be("https://canonical.url");
        }
    }
}
