using QingFa.EShop.Application.Features.Common.Responses;
using QingFa.EShop.Application.Features.Common.SeoInfo;

namespace QingFa.EShop.Application.Features.CategoryManagements.Models
{
    public record CategoryResponse : AuditEntityResponse<Guid>
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public string? ParentCategoryName { get; set; }
        public SeoMetaTransfer SeoMeta { get; set; } = default!;
        public IReadOnlyList<SubCategoryResponse> ChildCategories { get; set; } = [];
    }
}
