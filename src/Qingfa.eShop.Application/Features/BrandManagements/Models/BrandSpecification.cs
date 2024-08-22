using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Common.ValueObjects;
using QingFa.EShop.Domain.Core.Specifications;

using System.Linq.Expressions;

namespace QingFa.EShop.Application.Features.BrandManagements.Models
{
    public class BrandSpecification : SpecificationBase<Brand>
    {
        public BrandSpecification(
            string? name = null,
            IEnumerable<Guid>? ids = null,
            string? createdBy = null,
            SeoMeta? seoMeta = null)
        {
            // Build the base criteria expression
            Expression<Func<Category, bool>> baseCriteria = e => true;

            if (ids != null && ids.Any())
            {
                baseCriteria = e => ids.Contains(e.Id);
            }

            if (!string.IsNullOrEmpty(name))
            {
                var nameCriteria = Criteria != null
                    ? (Expression<Func<Brand, bool>>)(e => Criteria.Compile().Invoke(e) && e.Name.Contains(name))
                    : (e => e.Name.Contains(name));

                Criteria = nameCriteria;
            }

            if (!string.IsNullOrEmpty(createdBy))
            {
                var createdByCriteria = Criteria != null
                    ? (Expression<Func<Brand, bool>>)(e => Criteria.Compile().Invoke(e) && e.CreatedBy!.Contains(createdBy))
                    : (e => e.CreatedBy!.Contains(createdBy));

                Criteria = createdByCriteria;
            }

            if (seoMeta != null)
            {
                if (!string.IsNullOrEmpty(seoMeta.Title))
                {
                    var titleCriteria = Criteria != null
                        ? (Expression<Func<Brand, bool>>)(e => Criteria.Compile().Invoke(e) && e.SeoMeta.Title.Contains(seoMeta.Title))
                        : (e => e.SeoMeta.Title.Contains(seoMeta.Title));

                    Criteria = titleCriteria;
                }

                if (!string.IsNullOrEmpty(seoMeta.Description))
                {
                    var descriptionCriteria = Criteria != null
                        ? (Expression<Func<Brand, bool>>)(e => Criteria.Compile().Invoke(e) && e.SeoMeta.Description.Contains(seoMeta.Description))
                        : (e => e.SeoMeta.Description.Contains(seoMeta.Description));

                    Criteria = descriptionCriteria;
                }

                if (!string.IsNullOrEmpty(seoMeta.Keywords))
                {
                    var keywordsCriteria = Criteria != null
                        ? (Expression<Func<Brand, bool>>)(e => Criteria.Compile().Invoke(e) && e.SeoMeta.Keywords.Contains(seoMeta.Keywords))
                        : (e => e.SeoMeta.Keywords.Contains(seoMeta.Keywords));

                    Criteria = keywordsCriteria;
                }

                if (!string.IsNullOrEmpty(seoMeta.CanonicalUrl))
                {
                    var canonicalUrlCriteria = Criteria != null
                        ? (Expression<Func<Brand, bool>>)(e => Criteria.Compile().Invoke(e) && e.SeoMeta!.CanonicalUrl!.Contains(seoMeta.CanonicalUrl))
                        : (e => e.SeoMeta!.CanonicalUrl!.Contains(seoMeta.CanonicalUrl));

                    Criteria = canonicalUrlCriteria;
                }

                if (!string.IsNullOrEmpty(seoMeta.Robots))
                {
                    var robotsCriteria = Criteria != null
                        ? (Expression<Func<Brand, bool>>)(e => Criteria.Compile().Invoke(e) && e.SeoMeta!.Robots!.Contains(seoMeta.Robots))
                        : (e => e.SeoMeta!.Robots!.Contains(seoMeta.Robots));

                    Criteria = robotsCriteria;
                }
            }
        }
    }
}
