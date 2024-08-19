using Microsoft.EntityFrameworkCore;

using QingFa.EShop.Domain.Core.Repositories;
using QingFa.EShop.Domain.Core.Specifications;
using QingFa.EShop.Domain.Metas;
using QingFa.EShop.Infrastructure.Persistence;

namespace QingFa.EShop.Infrastructure.Repositories
{
    public class ExampleMetaRepository : IGenericRepository<ExampleMeta, Guid>
    {
        private readonly EShopDbContext _context;

        public ExampleMetaRepository(EShopDbContext context)
        {
            _context = context;
        }

        public async Task<ExampleMeta?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.ExampleMetas.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<IEnumerable<ExampleMeta>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.ExampleMetas.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<ExampleMeta>> ListAsync(ISpecification<ExampleMeta> specification, CancellationToken cancellationToken = default)
        {
            var query = _context.ExampleMetas.AsQueryable();

            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            foreach (var include in specification.Includes)
            {
                query = query.Include(include);
            }

            foreach (var includeString in specification.IncludeStrings)
            {
                query = query.Include(includeString);
            }

            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }

            if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(g => g);
            }

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

        public async Task AddAsync(ExampleMeta entity, CancellationToken cancellationToken = default)
        {
            await _context.ExampleMetas.AddAsync(entity, cancellationToken);
        }

        public async Task UpdateAsync(ExampleMeta entity, CancellationToken cancellationToken = default)
        {
            _context.ExampleMetas.Update(entity);
            await Task.CompletedTask;

        }

        public async Task DeleteAsync(ExampleMeta entity, CancellationToken cancellationToken = default)
        {
            _context.ExampleMetas.Remove(entity);
           await Task.CompletedTask;
        }
    }
}
