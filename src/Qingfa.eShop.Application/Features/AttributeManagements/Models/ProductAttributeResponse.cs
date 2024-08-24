using QingFa.EShop.Application.Features.AttributeOptionManagements.Models;
using QingFa.EShop.Application.Features.Common.Responses;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;

namespace QingFa.EShop.Application.Features.AttributeManagements.Models
{
    public record ProductAttributeResponse : AuditEntityResponse<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string AttributeCode { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ProductAttribute.AttributeType Type { get; set; }
        public bool IsRequired { get; set; }
        public bool IsFilterable { get; set; }
        public bool ShowToCustomers { get; set; }
        public int SortOrder { get; set; }
        public Guid AttributeGroupId { get; set; }
        public List<AttributeOptionResponse>? AttributeOptions { get; set; }
    }
}
