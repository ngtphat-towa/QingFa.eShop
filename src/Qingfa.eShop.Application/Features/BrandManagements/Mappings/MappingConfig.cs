using Mapster;

using QingFa.EShop.Application.Features.BrandManagements.CreateBrand;
using QingFa.EShop.Application.Features.BrandManagements.Models;
using QingFa.EShop.Application.Features.BrandManagements.Update;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Common.ValueObjects;

namespace QingFa.EShop.Application.Features.BrandManagements.Mappings
{
    internal static partial class MappingConfig
    {
        public static void ConfigureBrandMappings()
        {
            // Mapping from CreateBrandRequest DTO to CreateBrandCommand
            TypeAdapterConfig<CreateBrandRequest, CreateBrandCommand>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.SeoMeta, src => src.SeoMeta)
                .Map(dest => dest.LogoUrl, src => src.LogoUrl);

            // Mapping from UpdateBrandCommand to CreateBrandCommand
            TypeAdapterConfig<UpdateBrandRequest, UpdateBrandCommand>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.SeoMeta, src => src.SeoMeta)
                .Map(dest => dest.LogoUrl, src => src.LogoUrl);

            // Mapping from CreateBrandCommand to Brand domain model
            TypeAdapterConfig<Brand, BrandResponse>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.SeoMeta, src => SeoMeta.Create(
                    src.SeoMeta.Title ?? string.Empty,
                    src.SeoMeta.Description ?? string.Empty,
                    src.SeoMeta.Keywords ?? string.Empty,
                    src.SeoMeta.CanonicalUrl,
                    src.SeoMeta.Robots))
                .Map(dest => dest.LogoUrl, src => src.LogoUrl);

            TypeAdapterConfig<Brand, BasicBrandResponse>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name);
        }
    }
}
