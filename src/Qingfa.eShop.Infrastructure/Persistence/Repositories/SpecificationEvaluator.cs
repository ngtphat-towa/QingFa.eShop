using Microsoft.EntityFrameworkCore;

using QingFa.EShop.Domain.Specifications;

namespace QingFa.EShop.Infrastructure.Persistence.Repositories
{
    public class SpecificationEvaluator<T>
        where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;

            // Apply criteria for filtering entities
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            // Apply include expressions for related entities
            foreach (var include in specification.Includes)
            {
                query = query.Include(include);
            }

            // Apply include strings for related entities
            foreach (var includeString in specification.IncludeStrings)
            {
                query = query.Include(includeString);
            }

            // Apply ordering
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            // Apply grouping
            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
            }

            // Apply pagination if enabled
            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip ?? 0)
                             .Take(specification.Take ?? int.MaxValue);
            }

            return query;
        }
    }
}
