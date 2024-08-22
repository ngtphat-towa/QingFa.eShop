using QingFa.EShop.Application.Features.Common.SeoInfo;

namespace QingFa.EShop.Application.Features.CategoryManagements.CreateCategory
{
    public record CreateCategoryRequest
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public SeoMetaTransfer? SeoMeta { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}
