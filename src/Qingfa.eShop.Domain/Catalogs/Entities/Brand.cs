using QingFa.EShop.Domain.Common.ValueObjects;
using QingFa.EShop.Domain.Core.Entities;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    /// <summary>
    /// Represents a brand in the catalog, including associated SEO metadata, products, and a logo/image URL.
    /// </summary>
    public class Brand : BaseEntity<Guid>, IAuditable
    {
        /// <summary>
        /// Gets the name of the brand.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the description of the brand.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the SEO metadata associated with the brand.
        /// </summary>
        public SeoMeta SeoMeta { get; private set; }

        /// <summary>
        /// Gets the URL of the brand's logo or image.
        /// </summary>
        public string? LogoUrl { get; private set; }

        /// <summary>
        /// Gets the collection of products associated with the brand.
        /// </summary>
        public virtual ICollection<Product> Products { get; private set; }

        /// <summary>
        /// Gets or sets the creation date of the entity.
        /// </summary>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Gets or sets the user who created the entity.
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the last modification date of the entity.
        /// </summary>
        public DateTimeOffset LastModified { get; set; }

        /// <summary>
        /// Gets or sets the user who last modified the entity.
        /// </summary>
        public string? LastModifiedBy { get; set; }

        // Private parameterless constructor for EF Core
#pragma warning disable CS8618 
        private Brand()
#pragma warning restore CS8618 
            : base(Guid.NewGuid())
        {
            Products = [];
            SeoMeta = SeoMeta.CreateDefault();
            Created = DateTimeOffset.UtcNow;
            LastModified = DateTimeOffset.UtcNow;
        }

        // Private constructor for internal use
        private Brand(Guid id, string name, string description, SeoMeta seoMeta, string? logoUrl = null)
            : base(id)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            SeoMeta = seoMeta ?? throw new ArgumentNullException(nameof(seoMeta));
            LogoUrl = logoUrl;
            Products = new HashSet<Product>();
            Created = DateTimeOffset.UtcNow;
            LastModified = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Brand"/> with the specified name, description, SEO metadata, and logo URL.
        /// </summary>
        /// <param name="name">The name of the brand.</param>
        /// <param name="description">The description of the brand.</param>
        /// <param name="seoMeta">The SEO metadata for the brand.</param>
        /// <param name="logoUrl">The URL of the brand's logo or image.</param>
        /// <returns>A new instance of <see cref="Brand"/>.</returns>
        public static Brand Create(string name, string description, SeoMeta seoMeta, string? logoUrl = null)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            if (seoMeta == null) throw new ArgumentNullException(nameof(seoMeta));

            return new Brand(Guid.NewGuid(), name, description, seoMeta, logoUrl);
        }

        /// <summary>
        /// Creates a default instance of <see cref="Brand"/> with default values.
        /// </summary>
        /// <returns>A default instance of <see cref="Brand"/>.</returns>
        public static Brand CreateDefault()
        {
            return new Brand(Guid.NewGuid(), "Default Name", "Default Description", SeoMeta.CreateDefault(), null);
        }
    }
}
