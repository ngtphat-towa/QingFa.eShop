using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using QingFa.EShop.Domain.Core.Entities;
using QingFa.EShop.Domain.Core.Exceptions;
using QingFa.EShop.Domain.Core.Repositories;
using QingFa.EShop.Domain.Core.Specifications;

namespace QingFa.EShop.Infrastructure.Repositories.Common
{
    /// <summary>
    /// A generic repository for performing CRUD operations on entities.
    /// Implements the <see cref="IGenericRepository{TEntity, TId}"/> interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TId">The type of the entity's identifier.</typeparam>
    internal class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TId : notnull
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity, TId}"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Retrieves an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The entity with the specified identifier, or null if not found.</returns>
        public async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        }

        /// <summary>
        /// Retrieves all entities from the repository.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A read-only list of all entities.</returns>
        public async Task<IReadOnlyList<TEntity>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Retrieves entities that match the specified criteria.
        /// Applies filtering, sorting, grouping, and pagination based on the provided <paramref name="specification"/>.
        /// </summary>
        /// <param name="specification">The specification to filter, sort, and paginate entities.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A read-only list of entities that match the criteria.</returns>
        public async Task<IReadOnlyList<TEntity>> FindBySpecificationAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _dbSet;

            // Apply criteria
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            // Apply includes
            foreach (var includeExpression in specification.Includes)
            {
                query = query.Include(includeExpression);
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
            else if (specification.OrderByDescending != null)
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

        /// <summary>
        /// Counts the number of entities that match the specified criteria.
        /// Applies filtering and grouping based on the provided <paramref name="specification"/>.
        /// </summary>
        /// <param name="specification">The specification to filter and group entities for counting.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The count of entities that match the criteria.</returns>
        public async Task<int> CountBySpecificationAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _dbSet;

            // Apply criteria
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            // Apply grouping if needed
            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(g => g);
            }

            return await query.AsNoTracking().CountAsync(cancellationToken);
        }

        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Deletes an entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Checks if an entity exists by a specified field name and value.
        /// If no field name is specified, defaults to "Name".
        /// </summary>
        /// <param name="value">The value of the field to match.</param>
        /// <param name="fieldName">The name of the field to check. Defaults to "Name".</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>True if an entity with the specified field value exists; otherwise, false.</returns>
        public async Task<bool> ExistsByFieldAsync(object value, CancellationToken cancellationToken = default, string? fieldName = "Name")
        {
            if (string.IsNullOrWhiteSpace(fieldName))
                throw CoreException.NullOrEmptyArgument(nameof(fieldName));

            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, fieldName);
            var constant = Expression.Constant(value);
            var equals = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equals, parameter);

            return await _dbSet.AnyAsync(lambda, cancellationToken);
        }
    }
}
