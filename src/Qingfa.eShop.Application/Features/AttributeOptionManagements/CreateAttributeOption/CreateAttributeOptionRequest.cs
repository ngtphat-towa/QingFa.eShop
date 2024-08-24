namespace QingFa.EShop.Application.Features.AttributeOptionManagements.CreateAttributeOption
{
    public class CreateAttributeOptionRequest
    {
        public string OptionValue { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public bool IsDefault { get; init; }
        public int SortOrder { get; init; }
        public Guid? ProductAttributeId { get; init; }
    }
}
