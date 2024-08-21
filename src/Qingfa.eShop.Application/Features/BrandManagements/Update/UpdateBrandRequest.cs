using QingFa.EShop.Application.Features.Common.SeoInfo;

namespace QingFa.EShop.Application.Features.BrandManagements.Update
{
    public class UpdateBrandRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public SeoMetaTransfer SeoMeta { get; set; } = default!;
        public string? LogoUrl { get; set; }
    }
}
