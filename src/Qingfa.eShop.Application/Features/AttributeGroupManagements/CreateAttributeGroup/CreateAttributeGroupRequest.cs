namespace QingFa.EShop.Application.Features.AttributeGroupManagements.CreateAttributeGroup
{
    public record CreateAttributeGroupRequest
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
