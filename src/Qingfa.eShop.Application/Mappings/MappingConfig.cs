using Mapster;

using QingFa.EShop.Application.ExampleMetas.Models;
using QingFa.EShop.Application.Features.AttributeGroupManagements.CreateAttributeGroup;
using QingFa.EShop.Application.Features.AttributeGroupManagements.Models;
using QingFa.EShop.Application.Features.AttributeGroupManagements.UpdateAttributeGroup;
using QingFa.EShop.Application.Features.BrandManagements.CreateBrand;
using QingFa.EShop.Application.Features.BrandManagements.Models;
using QingFa.EShop.Application.Features.BrandManagements.Update;
using QingFa.EShop.Application.Features.CategoryManagements.CreateCategory;
using QingFa.EShop.Application.Features.CategoryManagements.Models;
using QingFa.EShop.Application.Features.CategoryManagements.UpdateCategory;
using QingFa.EShop.Application.Features.Common.Responses;
using QingFa.EShop.Application.Features.Common.SeoInfo;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Common.ValueObjects;
using QingFa.EShop.Domain.Core.Entities;
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
            ConfigureCategoryMappings();

            ConfigureAttributeGroupMappings();

            ConfigureAuditsEntityMappingMappings();
        }

        private static void ConfigureAttributeGroupMappings()
        {
            TypeAdapterConfig<ProductAttributeGroup, AttributeGroupResponse>
                .NewConfig()
                .Map(dest => dest.Name, src => src.GroupName);

            TypeAdapterConfig<CreateAttributeGroupRequest, CreateAttributeGroupCommand>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Description, src => src.Description);

            TypeAdapterConfig<UpdateAttributeGroupRequest, UpdateAttributeGroupCommand>
               .NewConfig()
               .Map(dest => dest.Name, src => src.Name)
               .Map(dest => dest.Description, src => src.Description)
               .Map(dest => dest.Status, src => src.Status);

        }

        private static void ConfigureAuditsEntityMappingMappings()
        {
            TypeAdapterConfig<AuditEntity, AuditEntityResponse<Guid>>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Created, src => src.Created)
                .Map(dest => dest.CreatedBy, src => src.CreatedBy)
                .Map(dest => dest.LastModified, src => src.LastModified)
                .Map(dest => dest.LastModifiedBy, src => src.LastModifiedBy)
                .Map(dest => dest.Status, src => src.Status);
        }

        private static void ConfigureExampleMetaMappings()
        {
            TypeAdapterConfig<ExampleMeta, ExampleMetaResponse>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name);

            TypeAdapterConfig<CreateExampleMetaRequest, ExampleMeta>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name);

            TypeAdapterConfig<UpdateExampleMetaRequest, ExampleMeta>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name);
        }
        private static void ConfigureBrandMappings()
        {
            TypeAdapterConfig<Brand, BrandResponse>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.SeoMeta, src => src.SeoMeta)
                .Map(dest => dest.LogoUrl, src => src.LogoUrl);

            TypeAdapterConfig<Brand, BasicBrandResponse>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name);

            TypeAdapterConfig<CreateBrandRequest, CreateBrandCommand>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.SeoMeta, src => src.SeoMeta)
                .Map(dest => dest.LogoUrl, src => src.LogoUrl);

            TypeAdapterConfig<UpdateBrandRequest, UpdateBrandCommand>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.SeoMeta, src => src.SeoMeta)
                .Map(dest => dest.LogoUrl, src => src.LogoUrl);
        }

        private static void ConfigureSeoMetaMappings()
        {
            TypeAdapterConfig<SeoMetaTransfer, SeoMeta>
                .NewConfig()
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Keywords, src => src.Keywords)
                .Map(dest => dest.CanonicalUrl, src => src.CanonicalUrl)
                .Map(dest => dest.Robots, src => src.Robots);

            TypeAdapterConfig<SeoMeta, SeoMetaResponse>
                .NewConfig()
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Keywords, src => src.Keywords)
                .Map(dest => dest.CanonicalUrl, src => src.CanonicalUrl)
                .Map(dest => dest.Robots, src => src.Robots);
        }
        private static void ConfigureCategoryMappings()
        {
            TypeAdapterConfig<Category, CategoryResponse>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.ImageUrl, src => src.ImageUrl)
                .Map(dest => dest.ParentCategoryId, src => src.ParentCategoryId)
                .Map(dest => dest.ParentCategoryName, src => src.ParentCategory != null ? src.ParentCategory.Name : default);

            TypeAdapterConfig<Category, SubCategoryResponse>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name);

            TypeAdapterConfig<CreateCategoryRequest, CreateCategoryCommand>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.ImageUrl, src => src.ImageUrl)
                .Map(dest => dest.ParentCategoryId, src => src.ParentCategoryId);

            TypeAdapterConfig<UpdateCategoryRequest, UpdateCategoryCommand>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.ImageUrl, src => src.ImageUrl)
                .Map(dest => dest.ParentCategoryId, src => src.ParentCategoryId);
        }

    }
}
