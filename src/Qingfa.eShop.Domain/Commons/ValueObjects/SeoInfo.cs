using QingFa.EShop.Domain.DomainModels.Core;

namespace QingFa.EShop.Domain.Commons.ValueObjects
{
    /// <summary>
    /// Represents SEO-related information for a catalog item.
    /// </summary>
    public class SeoInfo : ValueObject
    {
        #region Properties

        public string MetaTitle { get; private set; }
        public string MetaDescription { get; private set; }
        public string URLSlug { get; private set; }
        public string CanonicalURL { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Parameterless constructor for EF Core.
        /// </summary>
#pragma warning disable CS8618 
        private SeoInfo()
#pragma warning restore CS8618 
        {
            // EF Core requires a parameterless constructor.
        }

        /// <summary>
        /// Private constructor for creating a new instance with specified values.
        /// </summary>
        /// <param name="metaTitle">The meta title for SEO.</param>
        /// <param name="metaDescription">The meta description for SEO.</param>
        /// <param name="urlSlug">The URL slug for SEO.</param>
        /// <param name="canonicalURL">The canonical URL for SEO.</param>
        private SeoInfo(string metaTitle, string metaDescription, string urlSlug, string canonicalURL)
        {
            MetaTitle = metaTitle ?? throw new ArgumentNullException(nameof(metaTitle));
            MetaDescription = metaDescription ?? throw new ArgumentNullException(nameof(metaDescription));
            URLSlug = urlSlug ?? throw new ArgumentNullException(nameof(urlSlug));
            CanonicalURL = canonicalURL ?? throw new ArgumentNullException(nameof(canonicalURL));
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Creates a new instance of SeoInfo with specified values.
        /// </summary>
        /// <param name="metaTitle">The meta title for SEO.</param>
        /// <param name="metaDescription">The meta description for SEO.</param>
        /// <param name="urlSlug">The URL slug for SEO.</param>
        /// <param name="canonicalURL">The canonical URL for SEO.</param>
        /// <returns>A new instance of SeoInfo.</returns>
        public static SeoInfo Create(string metaTitle, string metaDescription, string urlSlug, string canonicalURL)
        {
            if (string.IsNullOrWhiteSpace(metaTitle))
                throw new ArgumentException("MetaTitle cannot be null or whitespace.", nameof(metaTitle));
            if (string.IsNullOrWhiteSpace(urlSlug))
                throw new ArgumentException("URLSlug cannot be null or whitespace.", nameof(urlSlug));

            return new SeoInfo(metaTitle, metaDescription, urlSlug, canonicalURL);
        }

        /// <summary>
        /// Creates a default instance of SeoInfo with preset values.
        /// </summary>
        /// <returns>A default instance of SeoInfo.</returns>
        public static SeoInfo CreateDefault()
        {
            return new SeoInfo(
                metaTitle: "Default Title",
                metaDescription: "Default Description",
                urlSlug: "default-url-slug",
                canonicalURL: "https://default-url.com"
            );
        }

        /// <summary>
        /// Creates an instance of SeoInfo specifically for a homepage.
        /// </summary>
        /// <param name="metaTitle">The meta title for the homepage.</param>
        /// <returns>An instance of SeoInfo for the homepage.</returns>
        public static SeoInfo CreateForHomepage(string metaTitle)
        {
            return new SeoInfo(
                metaTitle: metaTitle,
                metaDescription: "Homepage description.",
                urlSlug: "homepage",
                canonicalURL: "https://example.com/homepage"
            );
        }

        #endregion

        #region Methods

        /// <summary>
        /// Provides the atomic values of the object for equality comparison.
        /// </summary>
        /// <returns>An enumeration of atomic values.</returns>
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return MetaTitle;
            yield return MetaDescription;
            yield return URLSlug;
            yield return CanonicalURL;
        }

        #endregion
    }
}
