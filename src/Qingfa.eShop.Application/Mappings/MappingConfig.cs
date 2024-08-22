using Mapster;

using QingFa.EShop.Application.ExampleMetas.Models;
using QingFa.EShop.Application.Features.BrandManagements.Create;
using QingFa.EShop.Application.Features.BrandManagements.Models;
using QingFa.EShop.Application.Features.BrandManagements.Update;
using QingFa.EShop.Application.Features.CategoryManagements.CreateCategory;
using QingFa.EShop.Application.Features.CategoryManagements.Models;
using QingFa.EShop.Application.Features.CategoryManagements.UpdateCategories;
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
            #region ExampleMeta Mappings
            ConfigureExampleMetaMappings();
            #endregion

            #region Brand Mappings
            ConfigureBrandMappings();
            #endregion

            #region SEO Meta Mappings
            ConfigureSeoMetaMappings();
            #endregion

            #region Category Mappings
            ConfigureCategoryMappings();
            #endregion
        }

        #region ExampleMeta Mappings
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
        #endregion

        #region Brand Mappings
        private static void ConfigureBrandMappings()
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
        #endregion

        #region SEO Meta Mappings
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

            // Mapping from SeoMeta to SeoMetaResponse
            TypeAdapterConfig<SeoMeta, SeoMetaResponse>
                .NewConfig()
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Keywords, src => src.Keywords)
                .Map(dest => dest.CanonicalUrl, src => src.CanonicalUrl)
                .Map(dest => dest.Robots, src => src.Robots);
        }
        #endregion

        #region Category Mappings
        private static void ConfigureCategoryMappings()
        {
            // Mapping from Category domain model to CategoryResponse DTO
            TypeAdapterConfig<Category, CategoryResponse>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.ImageUrl, src => src.ImageUrl)
                .Map(dest => dest.ParentCategoryId, src => src.ParentCategoryId)
                .Map(dest => dest.ParentCategoryName, src => src.ParentCategory!=null ? src.ParentCategory.Name : default)
                .Map(dest => dest.Created, src => src.Created)
                .Map(dest => dest.CreatedBy, src => src.CreatedBy)
                .Map(dest => dest.LastModified, src => src.LastModified)
                .Map(dest => dest.LastModifiedBy, src => src.LastModifiedBy);

            TypeAdapterConfig<Category, SubCategoryResponse>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name);

            #region Request To Command
            // Mapping from CreateCategoryRequest DTO to CreateCategoryCommand
            TypeAdapterConfig<CreateCategoryRequest, CreateCategoryCommand>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.ImageUrl, src => src.ImageUrl)
                .Map(dest => dest.ParentCategoryId, src => src.ParentCategoryId);

            // Mapping from UpdateCategoryRequest DTO to UpdateCategoryCommand
            TypeAdapterConfig<UpdateCategoryRequest, UpdateCategoryCommand>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.ImageUrl, src => src.ImageUrl)
                .Map(dest => dest.ParentCategoryId, src => src.ParentCategoryId);
            #endregion

        }
        #endregion
    }
}
