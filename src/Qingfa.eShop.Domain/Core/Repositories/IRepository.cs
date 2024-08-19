using QingFa.EShop.Domain.Core.Entities;
using QingFa.EShop.Domain.Core.Specifications;

namespace QingFa.EShop.Domain.Core.Repositories
{
    public interface IGenericRepository<TEntity, TId> where TEntity : BaseEntity<TId> where TId : notnull
    {
        Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> ListAllAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
