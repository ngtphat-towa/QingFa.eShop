using QingFa.EShop.Domain.Core.Entities;
using QingFa.EShop.Domain.Core.Exceptions;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    public class Category : AuditEntity
    {
        public string Name { get; private set; } = default!;
        public string? Description { get; private set; }
        public string? ImageUrl { get; private set; }
        public Guid? ParentCategoryId { get; private set; }
        public virtual Category? ParentCategory { get; private set; }
        public virtual ICollection<Category> ChildCategories { get; private set; } = new HashSet<Category>();
        public virtual ICollection<CategoryProduct> CategoryProducts { get; private set; } = new HashSet<CategoryProduct>();


        private Category(Guid id, string name, string? description = null, string? imageUrl = null, Guid? parentCategoryId = null)
            : base(id)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
            ImageUrl = imageUrl;
            ParentCategoryId = parentCategoryId;
        }

        public static Category Create(string name, string? description = null, string? imageUrl = null, Guid? parentCategoryId = null)
        {
            if (string.IsNullOrWhiteSpace(name)) throw CoreException.NullOrEmptyArgument(nameof(name));
            return new Category(Guid.NewGuid(), name, description, imageUrl, parentCategoryId);
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

        public void AddChildCategory(Category childCategory)
        {
            if (childCategory == null) throw CoreException.NullArgument(nameof(childCategory));
            if (childCategory.Id == this.Id) throw CoreException.NullArgument(nameof(childCategory));
            if (ChildCategories.Any(c => c.Id == childCategory.Id)) throw new InvalidOperationException("The category is already a child.");

            ChildCategories.Add(childCategory);
        }

        public void RemoveChildCategory(Guid childCategoryId)
        {
            var childCategory = ChildCategories.FirstOrDefault(c => c.Id == childCategoryId);
            if (childCategory == null) throw CoreException.NotFound(nameof(ChildCategories));

            ChildCategories.Remove(childCategory);
        }

        public void ClearChildCategories()
        {
            ChildCategories.Clear();
        }
    }
}
