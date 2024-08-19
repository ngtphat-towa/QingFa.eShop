using QingFa.EShop.Domain.Metas;
using QingFa.EShop.Domain.Core.Specifications;
using System.Linq.Expressions;

namespace QingFa.EShop.Application.ExampleMetas.Models
{
    public class ExampleMetaSpecification : SpecificationBase<ExampleMeta>
    {
        public ExampleMetaSpecification(string? name = null, Guid? id = null, string? createdBy = null)
        {
            // Build the criteria based on the provided parameters
            if (id.HasValue)
            {
                Criteria = e => e.Id == id.Value;
            }

            if (!string.IsNullOrEmpty(name))
            {
                var nameCriteria = Criteria != null
                    ? (Expression<Func<ExampleMeta, bool>>)(e => Criteria.Compile().Invoke(e) && e.Name.Contains(name))
                    : (e => e.Name.Contains(name));

                Criteria = nameCriteria;
            }

            if (!string.IsNullOrEmpty(createdBy))
            {
                var createdByCriteria = Criteria != null
                    ? (Expression<Func<ExampleMeta, bool>>)(e => Criteria.Compile().Invoke(e) && e.CreatedBy!.Contains(createdBy))
                    : (e => e.CreatedBy!.Contains(createdBy));

                Criteria = createdByCriteria;
            }
        }
        public new void AddInclude(Expression<Func<ExampleMeta, object>> includeExpression)
        {
            base.AddInclude(includeExpression);
        }

        public new void AddInclude(string includeString)
        {
            base.AddInclude(includeString);
        }
    }
}
