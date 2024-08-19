namespace QingFa.EShop.Application.ExampleMetas.Models
{
    public record ExampleMetaResponse(
     Guid Id,
     string Name,
     DateTimeOffset Created,
     string? CreatedBy,
     DateTimeOffset LastModified,
     string? LastModifiedBy
 );
}
