using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Core.Specifications;

using System.Linq.Expressions;

namespace QingFa.EShop.Application.Features.AttributeGroupManagements.Models
{
    public class ProductAttributeGroupSpecification : SpecificationBase<ProductAttributeGroup>
    {
        public ProductAttributeGroupSpecification(
            string? name = null,
            string? description = null,
            IEnumerable<Guid>? ids = null,
            string? createdBy = null)
        {
            // Build the base criteria expression
            Expression<Func<ProductAttributeGroup, bool>> baseCriteria = e => true;

            if (ids != null && ids.Any())
            {
                baseCriteria = e => ids.Contains(e.Id);
            }

            if (!string.IsNullOrEmpty(name))
            {
                var nameCriteria = baseCriteria != null
                    ? (Expression<Func<ProductAttributeGroup, bool>>)(e => baseCriteria.Compile().Invoke(e) && e.GroupName.Contains(name))
                    : (e => e.GroupName.Contains(name));

                baseCriteria = nameCriteria;
            }

            if (!string.IsNullOrEmpty(createdBy))
            {
                var createdByCriteria = baseCriteria != null
                    ? (Expression<Func<ProductAttributeGroup, bool>>)(e => baseCriteria.Compile().Invoke(e) && e.CreatedBy != null && e.CreatedBy.Contains(createdBy))
                    : (e => e.CreatedBy != null && e.CreatedBy.Contains(createdBy));

                baseCriteria = createdByCriteria;
            }

            Criteria = baseCriteria;
        }
    }
}
