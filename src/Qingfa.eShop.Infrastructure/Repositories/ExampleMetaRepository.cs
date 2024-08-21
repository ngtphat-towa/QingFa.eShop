using Microsoft.EntityFrameworkCore;

using QingFa.EShop.Domain.Core.Specifications;
using QingFa.EShop.Domain.Metas;
using QingFa.EShop.Infrastructure.Persistence;

namespace QingFa.EShop.Infrastructure.Repositories
{
    public class ExampleMetaRepository : GenericRepository<ExampleMeta, Guid>, IExampleMetaRepository
    {
        private readonly EShopDbContext _context;

        public ExampleMetaRepository(EShopDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ExampleMeta>> ListAsync(ISpecification<ExampleMeta> specification, CancellationToken cancellationToken = default)
        {
            var query = _context.Set<ExampleMeta>().AsQueryable();

            // Apply criteria
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            // Apply includes
            foreach (var include in specification.Includes)
            {
                query = query.Include(include);
            }

            foreach (var includeString in specification.IncludeStrings)
            {
                query = query.Include(includeString);
            }

            // Apply sorting
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }

            if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            // Apply grouping
            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(g => g);
            }

            // Apply pagination
            if (specification.Skip.HasValue)
            {
                query = query.Skip(specification.Skip.Value);
            }

            if (specification.Take.HasValue)
            {
                query = query.Take(specification.Take.Value);
            }

            return await query.ToListAsync(cancellationToken);
        }
    }
}
