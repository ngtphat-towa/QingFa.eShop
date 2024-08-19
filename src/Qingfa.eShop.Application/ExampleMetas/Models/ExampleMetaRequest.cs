namespace QingFa.EShop.Application.ExampleMetas.Models
{
    public record CreateExampleMetaRequest(
         string Name,
         string? CreatedBy
     );

    public record UpdateExampleMetaRequest(
       Guid Id,
       string Name,
       string? LastModifiedBy
   );

    public record DeleteExampleMetaRequest(
       Guid Id
   );
}
