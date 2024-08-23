using QingFa.EShop.Domain.Core.Entities;
using QingFa.EShop.Domain.Core.Specifications;

namespace QingFa.EShop.Domain.Core.Repositories
{
    public interface IGenericRepository<TEntity, TId> where TEntity : BaseEntity<TId> where TId : notnull
    {
        /// <summary>
        /// Retrieves an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The entity with the specified identifier, or null if not found.</returns>
        Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all entities.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A read-only list of all entities.</returns>
        Task<IReadOnlyList<TEntity>> ListAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves entities that match the specified criteria.
        /// </summary>
        /// <param name="specification">The specification to filter entities.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A read-only list of entities that match the criteria.</returns>
        Task<IReadOnlyList<TEntity>> FindBySpecificationAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the count of entities that match the specified criteria.
        /// </summary>
        /// <param name="specification">The specification to filter entities.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The count of entities that match the criteria.</returns>
        Task<int> CountBySpecificationAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a new entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if an entity exists by a specified field name and value.
        /// If no field name is specified, defaults to "Name".
        /// </summary>
        /// <param name="value">The value of the field to match.</param>
        /// <param name="fieldName">The name of the field to check. Defaults to "Name".</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>True if an entity with the specified field value exists; otherwise, false.</returns>
        Task<bool> ExistsByFieldAsync(object value, CancellationToken cancellationToken = default, string fieldName = "Name");
    }
}
