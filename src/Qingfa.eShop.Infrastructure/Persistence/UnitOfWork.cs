using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

using QingFa.EShop.Domain.Core.Repositories;

namespace QingFa.EShop.Infrastructure.Persistence
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly EShopDbContext _dbContext;
        private readonly ILogger<UnitOfWork> _logger;
        private IDbContextTransaction? _currentTransaction;

        public UnitOfWork(EShopDbContext dbContext, ILogger<UnitOfWork> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Saving changes to the database.");
                var changes = await _dbContext.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Changes saved successfully. Number of entities affected: {NumberOfEntities}", changes);
                return changes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving changes.");
                throw;
            }
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }

            try
            {
                _logger.LogInformation("Beginning a new transaction.");
                _currentTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
                _logger.LogInformation("Transaction started successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while starting the transaction.");
                throw;
            }
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction == null)
            {
                throw new InvalidOperationException("No transaction is in progress.");
            }

            try
            {
                _logger.LogInformation("Committing the transaction.");
                await _currentTransaction.CommitAsync(cancellationToken);
                _logger.LogInformation("Transaction committed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while committing the transaction.");
                throw;
            }
            finally
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction == null)
            {
                throw new InvalidOperationException("No transaction is in progress.");
            }

            try
            {
                _logger.LogInformation("Rolling back the transaction.");
                await _currentTransaction.RollbackAsync(cancellationToken);
                _logger.LogInformation("Transaction rolled back successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while rolling back the transaction.");
                throw;
            }
            finally
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}
