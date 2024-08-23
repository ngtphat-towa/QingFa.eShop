using QingFa.EShop.Application.Features.Common.Requests;

namespace QingFa.EShop.Application.Features.AttributeGroupManagements.UpdateAttributeGroup
{
    public record UpdateAttributeGroupRequest: RequestType<Guid>
    {
        public string Name { get; set; } = default!;
    }
}
