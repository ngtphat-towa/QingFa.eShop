using QingFa.EShop.Domain.DomainModels.Repositories;

namespace QingFa.EShop.Domain.DomainModels.Interfaces
{
    public interface IUnitOfWork
    {
        // Access to repositories
        IGenericRepository<TEntity, TId> Repository<TEntity, TId>()
            where TEntity : Entity<TId>
            where TId : notnull;

        // Commit the transaction
        Task<int> CommitAsync(CancellationToken cancellationToken = default);

        // Rollback the transaction (if needed, typically managed manually in EF Core)
        Task RollbackAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    }
}
