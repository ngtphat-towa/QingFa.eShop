using QingFa.EShop.Application.Features.Common.Requests;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;

namespace QingFa.EShop.Application.Features.AttributeManagements.UpdateAttribute
{
    public record UpdateProductAttributeRequest: RequestType<Guid>
    {
        public string Name { get; init; } = string.Empty;
        public string AttributeCode { get; init; } = string.Empty;
        public string? Description { get; init; }
        public ProductAttribute.AttributeType Type { get; init; }
        public bool IsRequired { get; init; }
        public bool IsFilterable { get; init; }
        public bool ShowToCustomers { get; init; }
        public int SortOrder { get; init; }
        public Guid AttributeGroupId { get; init; }
    }
}
