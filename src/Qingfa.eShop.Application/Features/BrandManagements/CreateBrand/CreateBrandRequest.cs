using QingFa.EShop.Application.Features.Common.SeoInfo;
using QingFa.EShop.Domain.Core.Enums;

namespace QingFa.EShop.Application.Features.BrandManagements.CreateBrand
{
    public record CreateBrandRequest
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public SeoMetaTransfer SeoMeta { get; set; } = default!;
        public string? LogoUrl { get; set; }
        public EntityStatus Status { get; set; }
    }
}
