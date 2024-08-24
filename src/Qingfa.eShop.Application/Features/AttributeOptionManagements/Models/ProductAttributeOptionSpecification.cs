using System.Linq.Expressions;

using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Core.Specifications;

namespace QingFa.EShop.Application.Features.AttributeOptionManagements.Models
{
    public class ProductAttributeOptionSpecification : SpecificationBase<ProductAttributeOption>
    {
        public ProductAttributeOptionSpecification(
            IEnumerable<Guid>? ids = null,
            string? optionValue = null,
            string? description = null,
            bool? isDefault = null,
            int? sortOrder = null,
            Guid? productAttributeId = null,
            bool includeProductAttribute = false,
            string? searchTerm = null)
        {
            // Start with a base criteria that always evaluates to true
            Expression<Func<ProductAttributeOption, bool>> baseCriteria = e => true;

            // Collect conditions to apply
            var conditions = new List<Expression<Func<ProductAttributeOption, bool>>>();

            // Add conditions based on provided parameters
            if (ids != null && ids.Any())
            {
                conditions.Add(e => ids.Contains(e.Id));
            }

            if (!string.IsNullOrEmpty(optionValue))
            {
                conditions.Add(e => e.OptionValue.Contains(optionValue));
            }

            if (!string.IsNullOrEmpty(description))
            {
                conditions.Add(e => e.Description.Contains(description));
            }

            if (isDefault.HasValue)
            {
                conditions.Add(e => e.IsDefault == isDefault.Value);
            }

            if (sortOrder.HasValue)
            {
                conditions.Add(e => e.SortOrder == sortOrder.Value);
            }

            if (productAttributeId.HasValue)
            {
                conditions.Add(e => e.ProductAttributeId == productAttributeId.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Adding a condition for advanced search term
                conditions.Add(e => e.OptionValue.Contains(searchTerm) || e.Description.Contains(searchTerm));
            }

            // Combine all conditions using logical AND
            if (conditions.Any())
            {
                baseCriteria = conditions.Aggregate((current, next) =>
                    CombineExpressions(current, next, Expression.AndAlso));
            }

            Criteria = baseCriteria;

            // Conditionally include related entities based on flags
            if (includeProductAttribute)
            {
                AddInclude(o => o.Attribute);
            }
        }
    }
}
