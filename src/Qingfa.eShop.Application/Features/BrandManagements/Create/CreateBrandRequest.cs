using QingFa.EShop.Application.Features.Common.SeoInfo;

namespace QingFa.EShop.Application.Features.BrandManagements.Create
{
    public record CreateBrandRequest
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public SeoMetaTransfer SeoMeta { get; set; } = default!;
        public string? LogoUrl { get; set; }
    }
}
