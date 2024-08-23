using QingFa.EShop.Domain.Core.Enums;

namespace QingFa.EShop.Application.Features.Common.Responses
{
    public record AuditEntityResponse<IId> where IId : notnull
    {
        public IId Id { get; set; } = default!;
        public DateTimeOffset Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
        public EntityStatus Status { get; set; }
    }
}
