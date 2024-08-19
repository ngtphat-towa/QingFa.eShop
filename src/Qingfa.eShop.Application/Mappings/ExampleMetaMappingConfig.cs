using Mapster;

using QingFa.EShop.Application.ExampleMetas.Models;
using QingFa.EShop.Domain.Metas;

namespace QingFa.EShop.Application.Mappings
{
    public static  class MappingConfig
    {
        public static void ConfigureMappings()
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

            // Mapping from CreateExampleMetaRequest DTO to ExampleMeta domain model
            TypeAdapterConfig<CreateExampleMetaRequest, ExampleMeta>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.CreatedBy, src => src.CreatedBy)
                .Map(dest => dest.Created, src => DateTimeOffset.UtcNow) // Default Created time
                .Map(dest => dest.LastModified, src => DateTimeOffset.UtcNow) // Default LastModified time
                .Map(dest => dest.LastModifiedBy, src => src.CreatedBy); // Default LastModifiedBy

            // Mapping from UpdateExampleMetaRequest DTO to ExampleMeta domain model
            TypeAdapterConfig<UpdateExampleMetaRequest, ExampleMeta>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.LastModifiedBy, src => src.LastModifiedBy)
                .Map(dest => dest.LastModified, src => DateTimeOffset.UtcNow); // Update LastModified time

            // Mapping from DeleteExampleMetaRequest DTO to ExampleMeta domain model
            // Typically, the ID is used directly without mapping to a domain model
            TypeAdapterConfig<DeleteExampleMetaRequest, ExampleMeta>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id);


        }
    }
}
