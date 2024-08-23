using QingFa.EShop.Domain.Core.Entities;
using QingFa.EShop.Domain.Core.Exceptions;
using QingFa.EShop.Domain.Common.ValueObjects;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    public class Brand : AuditEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public SeoMeta SeoMeta { get; private set; }
        public string? LogoUrl { get; private set; }
        public virtual ICollection<Product> Products { get; private set; }

        // Private parameterless constructor for EF Core
#pragma warning disable CS8618 
        private Brand()
#pragma warning restore CS8618 
            : base(Guid.NewGuid())
        {
            Products = new HashSet<Product>();
            SeoMeta = SeoMeta.CreateDefault();
            Created = DateTimeOffset.UtcNow;
            LastModified = DateTimeOffset.UtcNow;
        }

        // Private constructor for internal use
        private Brand(Guid id, string name, string description, SeoMeta seoMeta, string? logoUrl = null)
            : base(id)
        {
            Name = name ?? throw CoreException.NullArgument(nameof(name));
            Description = description ?? throw CoreException.NullArgument(nameof(description));
            SeoMeta = seoMeta ?? throw CoreException.NullArgument(nameof(seoMeta));
            LogoUrl = logoUrl;
            Products = new HashSet<Product>();
            Created = DateTimeOffset.UtcNow;
            LastModified = DateTimeOffset.UtcNow;
        }

        public static Brand Create(string name, string description, SeoMeta seoMeta, string? logoUrl = null)
        {
            if (string.IsNullOrWhiteSpace(name)) throw CoreException.NullArgument(nameof(name));
            if (seoMeta == null) throw CoreException.NullArgument(nameof(seoMeta));

            return new Brand(Guid.NewGuid(), name, description, seoMeta, logoUrl);
        }

        public static Brand CreateDefault()
        {
            return new Brand(Guid.NewGuid(), "Default Name", "Default Description", SeoMeta.CreateDefault(), null);
        }
        public void UpdateName(string name)
        {
            Name = name ?? throw CoreException.NullArgument(nameof(name));
            LastModified = DateTimeOffset.UtcNow;
        }

        public void UpdateDescription(string description)
        {
            Description = description;
            LastModified = DateTimeOffset.UtcNow;
        }

        public void UpdateSeoMeta(SeoMeta seoMeta)
        {
            SeoMeta = seoMeta ?? throw CoreException.NullArgument(nameof(seoMeta));
            LastModified = DateTimeOffset.UtcNow;
        }

        public void UpdateLogoUrl(string? logoUrl)
        {
            LogoUrl = logoUrl;
            LastModified = DateTimeOffset.UtcNow;
        }
    }
}
