using Microsoft.EntityFrameworkCore;

using QingFa.EShop.Domain.DomainModels;
using QingFa.EShop.Domain.DomainModels.Repositories;
using QingFa.EShop.Domain.Specifications;

namespace QingFa.EShop.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId>
        where TEntity : Entity<TId>
        where TId : notnull
    {
        private readonly EShopDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(EShopDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            // Apply the specification using SpecificationEvaluator
            var query = SpecificationEvaluator<TEntity>.GetQuery(_dbSet.AsQueryable(), specification);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }
    }
}
