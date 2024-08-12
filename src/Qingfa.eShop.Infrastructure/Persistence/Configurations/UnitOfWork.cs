using Microsoft.Extensions.DependencyInjection;

using QingFa.EShop.Domain.DomainModels;
using QingFa.EShop.Domain.DomainModels.Interfaces;
using QingFa.EShop.Domain.DomainModels.Repositories;

namespace QingFa.EShop.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EShopDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(EShopDbContext context, IServiceProvider serviceProvider)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IGenericRepository<TEntity, TId> Repository<TEntity, TId>()
            where TEntity : Entity<TId>
            where TId : notnull
        {
            // Resolve the repository from the service provider
            return _serviceProvider.GetRequiredService<IGenericRepository<TEntity, TId>>();
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            // Commit the transaction
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            // Use transactions directly for manual rollback if needed
            // Example:
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                // Your database operations
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
