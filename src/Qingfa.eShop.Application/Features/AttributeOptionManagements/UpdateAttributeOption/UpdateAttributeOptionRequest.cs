using QingFa.EShop.Application.Features.Common.Requests;

namespace QingFa.EShop.Application.Features.AttributeOptionManagements.UpdateAttributeOption
{
    public record UpdateAttributeOptionRequest : RequestType<Guid>
    {
        public string OptionValue { get; init; } = string.Empty;
        public string? Description { get; init; }
        public bool IsDefault { get; init; }
        public int SortOrder { get; init; }
        public Guid ProductAttributeId { get; init; }
    }
}
