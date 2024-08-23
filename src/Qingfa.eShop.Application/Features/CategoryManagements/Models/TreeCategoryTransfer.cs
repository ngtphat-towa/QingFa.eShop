using QingFa.EShop.Application.Features.Common.Responses;

namespace QingFa.EShop.Application.Features.CategoryManagements.Models
{
    public record TreeCategoryTransfer:BasicResponse<Guid>
    {
        public IReadOnlyList<TreeCategoryTransfer> SubCategories { get; set; } = [];
    }
}
