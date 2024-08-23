using QingFa.EShop.Application.Features.Common.Responses;
using QingFa.EShop.Application.Features.Common.SeoInfo;

namespace QingFa.EShop.Application.Features.BrandManagements.Models
{
    public record BrandResponse : AuditEntityResponse<Guid>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public SeoMetaResponse SeoMeta { get; set; } = default!;
        public string? LogoUrl { get; set; }
    }

    public record BasicBrandResponse : BasicResponse<Guid>;
}
