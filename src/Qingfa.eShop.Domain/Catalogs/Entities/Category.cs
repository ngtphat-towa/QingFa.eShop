using QingFa.EShop.Domain.Core.Entities;
using QingFa.EShop.Domain.Core.Exceptions;
using QingFa.EShop.Domain.Core.Enums;
using QingFa.EShop.Domain.Common.ValueObjects;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    public class Category : AuditEntity
    {
        public string Name { get; private set; } = default!;
        public string? Description { get; private set; }
        public string? ImageUrl { get; private set; }
        public SeoMeta SeoMeta { get; private set; } = SeoMeta.CreateDefault();
        public Guid? ParentCategoryId { get; private set; }
        public virtual Category ParentCategory { get; private set; } = default!;
        public virtual ICollection<Category> ChildCategories { get; private set; } = new HashSet<Category>();
        public virtual ICollection<CategoryProduct> CategoryProducts { get; private set; } = new HashSet<CategoryProduct>();

        private Category(Guid id, string name, string? description = null, string? imageUrl = null, Guid? parentCategoryId = null, SeoMeta? seoMeta = null, EntityStatus status = EntityStatus.Active)
            : base(id)
        {
            Name = name ?? throw CoreException.NullOrEmptyArgument(nameof(name));
            Description = description;
            ImageUrl = imageUrl;
            ParentCategoryId = parentCategoryId;
            SeoMeta = seoMeta ?? SeoMeta.CreateDefault();
            Status = status;
        }

        private Category(): base(default!)
        {
        }

        public static Category Create(string name, string? description = null, string? imageUrl = null, Guid? parentCategoryId = null, SeoMeta? seoMeta = null)
        {
            if (string.IsNullOrWhiteSpace(name)) throw CoreException.NullOrEmptyArgument(nameof(name));
            return new Category(Guid.NewGuid(), name, description, imageUrl, parentCategoryId, seoMeta);
        }

        public void Update(string name, string? description = null, string? imageUrl = null, Guid? parentCategoryId = null, string? lastModifiedBy = null)
        {
            if (string.IsNullOrWhiteSpace(name)) throw CoreException.NullOrEmptyArgument(nameof(name));

            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            ParentCategoryId = parentCategoryId;

            UpdateAuditInfo(lastModifiedBy);
        }

        public void UpdateSeoMeta(SeoMeta seoMeta)
        {
            SeoMeta = seoMeta ?? throw CoreException.NullArgument(nameof(seoMeta));
            LastModified = DateTimeOffset.UtcNow;
        }

        public void AddChildCategory(Category newChildCategory)
        {
            if (this == null)
            {
                throw CoreException.NullArgument("Parent category");
            }

            if (newChildCategory == null)
            {
                throw CoreException.NullArgument("New child category");
            }

            if (this.ChildCategories.Any(c => c.Id == newChildCategory.Id))
            {
                return;
            }

            this.ChildCategories.Add(newChildCategory);
        }

        public void RemoveChildCategory(Guid childCategoryId)
        {
            if (ChildCategories == null || !ChildCategories.Any())
            {
                throw CoreException.NullArgument("Parent category");
            }

            var childCategory = ChildCategories.SingleOrDefault(c => c.Id == childCategoryId);
            if (childCategory == null)
            {
                return;
            }

            ChildCategories.Remove(childCategory);
        }



        public void ClearChildCategories()
        {
            ChildCategories.Clear();
        }
    }
}
