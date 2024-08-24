namespace QingFa.EShop.Application.Features.AttributeManagements.AssignAttributeOptions
{
    public record AssignAttributeOptionsRequest
    {
        public Guid ProductAttributeId { get; init; }
        public List<Guid> AttributeOptionIds { get; init; } = new List<Guid>();
    }
}
