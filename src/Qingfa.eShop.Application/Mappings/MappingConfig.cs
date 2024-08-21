using Mapster;

using QingFa.EShop.Application.ExampleMetas.Models;
using QingFa.EShop.Application.Features.BrandManagements.Create;
using QingFa.EShop.Application.Features.Common.SeoInfo;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Common.ValueObjects;
using QingFa.EShop.Domain.Metas;

namespace QingFa.EShop.Application.Mappings
{
    public static class MappingConfig
    {
        public static void ConfigureMappings()
        {
            ConfigureExampleMetaMappings();
            ConfigureBrandMappings();
            ConfigureSeoMetaMappings();
        }

        private static void ConfigureExampleMetaMappings()
        {
            // Mapping from ExampleMeta domain model to ExampleMetaResponse DTO
            TypeAdapterConfig<ExampleMeta, ExampleMetaResponse>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Created, src => src.Created)
                .Map(dest => dest.CreatedBy, src => src.CreatedBy)
                .Map(dest => dest.LastModified, src => src.LastModified)
                .Map(dest => dest.LastModifiedBy, src => src.LastModifiedBy);

            // Mapping from ExampleMetaRequest DTOs to ExampleMeta domain model
            TypeAdapterConfig<CreateExampleMetaRequest, ExampleMeta>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.CreatedBy, src => src.CreatedBy)
                .Map(dest => dest.Created, _ => DateTimeOffset.UtcNow)
                .Map(dest => dest.LastModified, _ => DateTimeOffset.UtcNow)
                .Map(dest => dest.LastModifiedBy, src => src.CreatedBy);

            TypeAdapterConfig<UpdateExampleMetaRequest, ExampleMeta>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.LastModifiedBy, src => src.LastModifiedBy)
                .Map(dest => dest.LastModified, _ => DateTimeOffset.UtcNow);

            TypeAdapterConfig<DeleteExampleMetaRequest, ExampleMeta>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id);
        }

        private static void ConfigureBrandMappings()
        {
            // Mapping from CreateBrandRequest DTO to CreateBrandCommand
            TypeAdapterConfig<CreateBrandRequest, CreateBrandCommand>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.SeoMeta, src => src.SeoMeta)
                .Map(dest => dest.LogoUrl, src => src.LogoUrl);

            // Mapping from CreateBrandCommand to Brand domain model
            TypeAdapterConfig<CreateBrandCommand, Brand>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.SeoMeta, src => SeoMeta.Create(
                    src.SeoMeta.Title,
                    src.SeoMeta.Description,
                    src.SeoMeta.Keywords,
                    src.SeoMeta.CanonicalUrl,
                    src.SeoMeta.Robots))
                .Map(dest => dest.LogoUrl, src => src.LogoUrl);
        }

        private static void ConfigureSeoMetaMappings()
        {
            // Mapping from SeoMetaTransfer to SeoMeta
            TypeAdapterConfig<SeoMetaTransfer, SeoMeta>
                .NewConfig()
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Keywords, src => src.Keywords)
                .Map(dest => dest.CanonicalUrl, src => src.CanonicalUrl)
                .Map(dest => dest.Robots, src => src.Robots);

            // Mapping from SeoMeta to SeoMetaTransfer
            TypeAdapterConfig<SeoMeta, SeoMetaTransfer>
                .NewConfig()
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Keywords, src => src.Keywords)
                .Map(dest => dest.CanonicalUrl, src => src.CanonicalUrl)
                .Map(dest => dest.Robots, src => src.Robots);
        }
    }
}
