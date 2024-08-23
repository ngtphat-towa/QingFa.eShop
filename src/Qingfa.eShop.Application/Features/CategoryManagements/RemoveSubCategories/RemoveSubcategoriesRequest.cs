namespace QingFa.EShop.Application.Features.CategoryManagements.RemoveSubCategories
{
    public class RemoveSubcategoriesRequest
    {
        public Guid ParentCategoryId { get; init; }
        public List<Guid> SubcategoryIds { get; init; } = new();
    }
}
