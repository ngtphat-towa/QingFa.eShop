using QingFa.EShop.Application.Features.Common.SeoInfo;

namespace QingFa.EShop.Application.Features.CategoryManagements.Models
{
    public record CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public string? ParentCategoryName { get; set; }
        public SeoMetaTransfer SeoMeta { get; set; } = default!;
        public IReadOnlyList<SubCategoryResponse> ChildCategories { get; set; } = [];
        public DateTimeOffset Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
