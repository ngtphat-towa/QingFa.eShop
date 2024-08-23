using System.Linq.Expressions;

using QingFa.EShop.Application.Features.Common.SeoInfo;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Core.Enums;
using QingFa.EShop.Domain.Core.Specifications;

namespace QingFa.EShop.Application.Features.CategoryManagements.Models
{
    public class CategorySpecification : SpecificationBase<Category>
    {
        public CategorySpecification(
            IEnumerable<Guid>? ids = null,
            string? name = null,
            Guid? parentCategoryId = null,
            string? description = null,
            IEnumerable<EntityStatus>? statuses = null,
            SeoMetaTransfer? seoMeta = null,
            bool includeChildCategories = false,
            bool includeParentCategory = false)
        {
            // Start with a base criteria that always evaluates to true
            Expression<Func<Category, bool>> baseCriteria = e => true;

            // Collect conditions to apply
            var conditions = new List<Expression<Func<Category, bool>>>();

            // Add conditions based on provided parameters
            if (ids != null && ids.Any())
            {
                conditions.Add(e => ids.Contains(e.Id));
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

            if (seoMeta != null)
            {
                if (!string.IsNullOrEmpty(seoMeta.Title))
                {
                    conditions.Add(e => e.SeoMeta.Title.Contains(seoMeta.Title));
                }

                if (!string.IsNullOrEmpty(seoMeta.Description))
                {
                    conditions.Add(e => e.SeoMeta.Description.Contains(seoMeta.Description));
                }

                if (!string.IsNullOrEmpty(seoMeta.Keywords))
                {
                    conditions.Add(e => e.SeoMeta.Keywords.Contains(seoMeta.Keywords));
                }

                if (!string.IsNullOrEmpty(seoMeta.CanonicalUrl))
                {
                    conditions.Add(e => e.SeoMeta.CanonicalUrl!.Contains(seoMeta.CanonicalUrl));
                }

                if (!string.IsNullOrEmpty(seoMeta.Robots))
                {
                    conditions.Add(e => e.SeoMeta.Robots!.Contains(seoMeta.Robots));
                }
            }

            // Add status condition if any statuses are provided
            if (statuses != null && statuses.Any())
            {
                conditions.Add(e => statuses.Contains(e.Status));
            }

            // Combine all conditions using logical AND
            if (conditions.Any())
            {
                baseCriteria = conditions.Aggregate((current, next) =>
                    CombineExpressions(current, next, Expression.AndAlso));
            }

            Criteria = baseCriteria;

            // Conditionally include related entities based on flags
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
