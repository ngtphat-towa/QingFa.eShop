using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    public class CatalogCategory : Entity<CatalogCategoryId>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public CatalogCategoryId? ParentCategoryId { get; private set; }
        public virtual CatalogCategory? ParentCategory { get; private set; }

        private CatalogCategory(CatalogCategoryId catalogCategoryId, string name, string description, CatalogCategoryId? parentCategoryId = null)
            : base(catalogCategoryId)
        {
            Name = name ?? throw CoreException.NullArgument(nameof(name));
            Description = description;
            ParentCategoryId = parentCategoryId;
        }

        public static CatalogCategory Create(CatalogCategoryId catalogCategoryId, string name, string description, CatalogCategoryId? parentCategoryId = null)
        {
            if (string.IsNullOrWhiteSpace(name)) throw CoreException.NullArgument(nameof(name));
            return new CatalogCategory(catalogCategoryId, name, description, parentCategoryId);
        }

        public void UpdateDescription(string description)
        {
            Description = description;
        }

        public void SetParentCategory(CatalogCategory? parentCategory)
        {
            if (parentCategory == null && ParentCategoryId != null)
            {
                // Handle case where you are trying to remove the parent category
                ParentCategoryId = null;
            }
            else if (parentCategory != null)
            {
                ParentCategory = parentCategory;
                ParentCategoryId = parentCategory.Id;
            }
            else
            {
                throw CoreException.NullArgument(nameof(parentCategory));
            }
        }
#pragma warning disable CS8618
        protected CatalogCategory()
#pragma warning restore CS8618
        {
            
        }
    }
}
