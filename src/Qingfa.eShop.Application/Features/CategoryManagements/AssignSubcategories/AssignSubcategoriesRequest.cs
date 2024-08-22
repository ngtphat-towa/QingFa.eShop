namespace QingFa.EShop.Application.Features.CategoryManagements.AssignSubcategories
{
    public record AssignSubcategoriesRequest
    {
        public Guid ParentCategoryId { get; init; }
        public List<Guid> SubcategoryIds { get; init; } = [];
    }
}
