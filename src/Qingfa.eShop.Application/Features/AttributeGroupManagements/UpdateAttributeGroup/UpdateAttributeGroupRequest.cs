using QingFa.EShop.Application.Features.Common.Requests;
using QingFa.EShop.Domain.Core.Enums;

namespace QingFa.EShop.Application.Features.AttributeGroupManagements.UpdateAttributeGroup
{
    public record UpdateAttributeGroupRequest: RequestType<Guid>
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public EntityStatus? Status { get; set; } = default!;

    }
}
