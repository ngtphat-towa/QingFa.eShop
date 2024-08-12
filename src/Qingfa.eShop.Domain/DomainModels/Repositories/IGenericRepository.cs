using QingFa.EShop.Domain.Specifications;

namespace QingFa.EShop.Domain.DomainModels.Repositories
{
    public interface IGenericRepository<TEntity, TId>
        where TEntity : Entity<TId>
        where TId : notnull
    {
        // Retrieve an entity by its ID
        Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

        // Retrieve all entities
        Task<IEnumerable<TEntity>> ListAllAsync(CancellationToken cancellationToken = default);

        // Retrieve entities based on a specification
        Task<IEnumerable<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        // Add a new entity
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        // Update an existing entity
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        // Delete an existing entity
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
