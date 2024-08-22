using System.Linq.Expressions;

using QingFa.EShop.Application.Features.Common.SeoInfo;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Core.Specifications;

namespace QingFa.EShop.Application.Features.CategoryManagements.Models
{
    public class CategorySpecification : SpecificationBase<Category>
    {
        public CategorySpecification(
            IEnumerable<Guid>? ids = null,
            string? name = null,
            Guid? id = null,
            Guid? parentCategoryId = null,
            string? description = null,
            string? imageUrl = null,
            SeoMetaTransfer? seoMetaTransfer = null,
            bool includeChildCategories = false,
            bool includeParentCategory = false)
        {
            // Build the base criteria expression
            Expression<Func<Category, bool>> baseCriteria = e => true;

            // Create a list of conditions to combine
            var conditions = new List<Expression<Func<Category, bool>>>();

            if (ids != null && ids.Any())
            {
                conditions.Add(e => ids.Contains(e.Id));
            }

            if (id.HasValue)
            {
                conditions.Add(e => e.Id == id.Value);
            }

            if (!string.IsNullOrEmpty(name))
            {
                conditions.Add(e => e.Name.Contains(name));
            }

            if (parentCategoryId.HasValue)
            {
                conditions.Add(e => e.ParentCategoryId == parentCategoryId.Value);
            }

            if (!string.IsNullOrEmpty(description))
            {
                conditions.Add(e => e.Description != null && e.Description.Contains(description));
            }

            if (!string.IsNullOrEmpty(imageUrl))
            {
                conditions.Add(e => e.ImageUrl != null && e.ImageUrl.Contains(imageUrl));
            }

            // Combine all conditions using logical AND
            if (conditions.Any())
            {
                baseCriteria = conditions.Aggregate((current, next) =>
                    CombineExpressions(current, next, Expression.AndAlso));
            }

            Criteria = baseCriteria;

            // Conditionally include related entities
            if (includeChildCategories)
            {
                AddInclude(c => c.ChildCategories);
            }

            if (includeParentCategory)
            {
                AddInclude(c => c.ParentCategory);
            }
        }
    }
}