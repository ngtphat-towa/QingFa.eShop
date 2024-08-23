using QingFa.EShop.Application.Features.Common.Responses;

namespace QingFa.EShop.Application.Features.AttributeGroupManagements.Models
{
    public record AttributeGroupResponse : AuditEntityResponse<Guid>
    {
        public string Name { get; set; } = default!;
    }
}
