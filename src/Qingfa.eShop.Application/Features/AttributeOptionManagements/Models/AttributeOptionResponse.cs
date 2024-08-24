using QingFa.EShop.Application.Features.Common.Responses;

namespace QingFa.EShop.Application.Features.AttributeOptionManagements.Models
{
    public record AttributeOptionResponse : AuditEntityResponse<Guid>
    {
        public string OptionValue { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
        public int SortOrder { get; set; }
        public Guid ProductAttributeId { get; set; }
    }
}
