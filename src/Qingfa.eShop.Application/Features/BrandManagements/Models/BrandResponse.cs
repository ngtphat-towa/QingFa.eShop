using QingFa.EShop.Application.Features.Common.SeoInfo;

namespace QingFa.EShop.Application.Features.BrandManagements.Models
{
    public record BrandResponse(
        Guid Id,
        string Name,
        string Description,
        SeoMetaResponse SeoMeta,
        string? LogoUrl);
}
