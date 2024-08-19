using QingFa.EShop.Domain.Core.ValueObjects;

namespace QingFa.EShop.Domain.Common.ValueObjects
{
    /// <summary>
    /// Represents SEO metadata for a web page or content.
    /// </summary>
    public class SeoMeta : ValueObject
    {
        /// <summary>
        /// Gets the title of the page or content, used for SEO purposes.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the description of the page or content, used for SEO purposes.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the keywords associated with the page or content, used for SEO purposes.
        /// </summary>
        public string Keywords { get; private set; }

        /// <summary>
        /// Gets the canonical URL for the page, used to avoid duplicate content issues.
        /// </summary>
        public string? CanonicalUrl { get; private set; }

        /// <summary>
        /// Gets the robots meta tag value for controlling search engine indexing and crawling.
        /// </summary>
        public string? Robots { get; private set; }

        // Private parameterless constructor for EF Core
        private SeoMeta()
            : this("Default Title", "Default description for SEO.", "default,keywords", null, null)
        {
        }

        // Private constructor for internal use
        private SeoMeta(string title, string description, string keywords, string? canonicalUrl = null, string? robots = null)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Keywords = keywords ?? throw new ArgumentNullException(nameof(keywords));
            CanonicalUrl = canonicalUrl;
            Robots = robots;
        }

        /// <summary>
        /// Creates a new instance of <see cref="SeoMeta"/> with the specified values.
        /// </summary>
        /// <param name="title">The title of the page or content.</param>
        /// <param name="description">The description of the page or content.</param>
        /// <param name="keywords">The keywords associated with the page or content.</param>
        /// <param name="canonicalUrl">The canonical URL for the page.</param>
        /// <param name="robots">The robots meta tag value.</param>
        /// <returns>A new instance of <see cref="SeoMeta"/>.</returns>
        public static SeoMeta Create(string title, string description, string keywords, string? canonicalUrl = null, string? robots = null)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be null or empty.", nameof(title));
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description cannot be null or empty.", nameof(description));
            if (string.IsNullOrWhiteSpace(keywords)) throw new ArgumentException("Keywords cannot be null or empty.", nameof(keywords));

            return new SeoMeta(title, description, keywords, canonicalUrl, robots);
        }

        /// <summary>
        /// Creates a default instance of <see cref="SeoMeta"/> with default values.
        /// </summary>
        /// <returns>A default instance of <see cref="SeoMeta"/>.</returns>
        public static SeoMeta CreateDefault()
        {
            return new SeoMeta();
        }

        /// <summary>
        /// Provides the components for equality comparison.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of components used for equality comparison.</returns>
        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Title;
            yield return Description;
            yield return Keywords;
            yield return CanonicalUrl;
            yield return Robots;
        }
    }
}
