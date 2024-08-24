using System.Linq.Expressions;

using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Core.Specifications;

using static QingFa.EShop.Domain.Catalogs.Entities.Attributes.ProductAttribute;

namespace QingFa.EShop.Application.Features.AttributeManagements.Models
{
    public class ProductAttributeSpecification : SpecificationBase<ProductAttribute>
    {
        public ProductAttributeSpecification(
            IEnumerable<Guid>? ids = null,
            string? name = null,
            string? attributeCode = null,
            AttributeType? type = null,
            bool? isRequired = null,
            bool? isFilterable = null,
            bool? showToCustomers = null,
            int? sortOrder = null,
            Guid? attributeGroupId = null,
            bool includeAttributeOptions = false,
            bool includeVariantAttributes = false,
            string? searchTerm = null)
        {
            // Start with a base criteria that always evaluates to true
            Expression<Func<ProductAttribute, bool>> baseCriteria = e => true;

            // Collect conditions to apply
            var conditions = new List<Expression<Func<ProductAttribute, bool>>>();

            // Add conditions based on provided parameters
            if (ids != null && ids.Any())
            {
                conditions.Add(e => ids.Contains(e.Id));
            }

            if (!string.IsNullOrEmpty(name))
            {
                conditions.Add(e => e.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(attributeCode))
            {
                conditions.Add(e => e.AttributeCode.Contains(attributeCode));
            }

            if (type.HasValue)
            {
                conditions.Add(e => e.Type == type.Value);
            }

            if (isRequired.HasValue)
            {
                conditions.Add(e => e.IsRequired == isRequired.Value);
            }

            if (isFilterable.HasValue)
            {
                conditions.Add(e => e.IsFilterable == isFilterable.Value);
            }

            if (showToCustomers.HasValue)
            {
                conditions.Add(e => e.ShowToCustomers == showToCustomers.Value);
            }

            if (sortOrder.HasValue)
            {
                conditions.Add(e => e.SortOrder == sortOrder.Value);
            }

            if (attributeGroupId.HasValue)
            {
                conditions.Add(e => e.AttributeGroupId == attributeGroupId.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Adding a condition for advanced search term
                conditions.Add(e => e.Name.Contains(searchTerm) || e.AttributeCode.Contains(searchTerm));
            }

            // Combine all conditions using logical AND
            if (conditions.Any())
            {
                baseCriteria = conditions.Aggregate((current, next) =>
                    CombineExpressions(current, next, Expression.AndAlso));
            }

            Criteria = baseCriteria;

            // Conditionally include related entities based on flags
            if (includeAttributeOptions)
            {
                AddInclude(a => a.AttributeOptions);
            }

            if (includeVariantAttributes)
            {
                AddInclude(a => a.VariantAttributes);
            }
        }
    }
}
