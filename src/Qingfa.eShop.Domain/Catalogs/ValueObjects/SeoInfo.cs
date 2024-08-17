using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.ObjectValues
{
    public class SeoInfo : ValueObject
    {
        public string MetaTitle { get; private set; } = default!;
        public string MetaDescription { get; private set; } = default!;
        public string URLSlug { get; private set; } = default!;
        public string CanonicalURL { get; private set; } = default!;

        // Parameterless constructor for EF Core
        private SeoInfo()
        {
        }

        // Private constructor for use with static factory methods
        private SeoInfo(string metaTitle, string metaDescription, string urlSlug, string canonicalURL)
        {
            MetaTitle = metaTitle;
            MetaDescription = metaDescription;
            URLSlug = urlSlug;
            CanonicalURL = canonicalURL;
        }

        // Static factory method for creating an instance of SeoInfo
        public static SeoInfo Create(string metaTitle, string metaDescription, string urlSlug, string canonicalURL)
        {
            // Add validation logic if needed
            if (string.IsNullOrWhiteSpace(metaTitle))
                throw new ArgumentException("MetaTitle cannot be null or whitespace.", nameof(metaTitle));
            if (string.IsNullOrWhiteSpace(urlSlug))
                throw new ArgumentException("URLSlug cannot be null or whitespace.", nameof(urlSlug));

            return new SeoInfo(metaTitle, metaDescription, urlSlug, canonicalURL);
        }

        // Additional static factory methods for common use cases
        public static SeoInfo CreateDefault()
        {
            return new SeoInfo(
                metaTitle: "Default Title",
                metaDescription: "Default Description",
                urlSlug: "default-url-slug",
                canonicalURL: "https://default-url.com"
            );
        }

        public static SeoInfo CreateForHomepage(string metaTitle)
        {
            return new SeoInfo(
                metaTitle: metaTitle,
                metaDescription: "Homepage description.",
                urlSlug: "homepage",
                canonicalURL: "https://example.com/homepage"
            );
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Return all the properties that determine equality
            yield return MetaTitle;
            yield return MetaDescription;
            yield return URLSlug;
            yield return CanonicalURL;
        }
    }
}
