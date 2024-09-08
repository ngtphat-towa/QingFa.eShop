using QingFa.EShop.Application.Features.Common.Requests;

namespace QingFa.EShop.Application.Features.AttributeOptionManagements.Models
{
    public record BasicAttributeOptionResponse : RequestType<Guid>
    {
        public string OptionValue { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
        public int SortOrder { get; set; }
    }
}
