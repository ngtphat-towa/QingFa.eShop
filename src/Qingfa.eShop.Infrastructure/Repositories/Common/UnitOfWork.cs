//using Microsoft.EntityFrameworkCore.Storage;
//using Microsoft.Extensions.Logging;

//using QingFa.EShop.Domain.Core.Repositories;
//using QingFa.EShop.Infrastructure.Persistence.Data;
//using QingFa.EShop.Infrastructure.Identity;
//using Microsoft.EntityFrameworkCore;

//namespace QingFa.EShop.Infrastructure.Repositories.Common
//{
//    internal class UnitOfWork : IUnitOfWork
//    {
//        private readonly EShopDbContext _dbContext;
//        private readonly IdentityDataDbContext _identityDbContext;
//        private readonly ILogger<UnitOfWork> _logger;
//        private IDbContextTransaction? _currentTransaction;

//        public UnitOfWork(EShopDbContext dbContext, IdentityDataDbContext identityDbContext, ILogger<UnitOfWork> logger)
//        {
//            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
//            _identityDbContext = identityDbContext ?? throw new ArgumentNullException(nameof(identityDbContext));
//            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//        }

//        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
//        {
//            try
//            {
//                _logger.LogInformation("Saving changes to the database.");
//                int totalChanges = 0;

//                if (_dbContext.ChangeTracker.HasChanges())
//                {
//                    var dbContextChanges = await _dbContext.SaveChangesAsync(cancellationToken);
//                    totalChanges += dbContextChanges;
//                    _logger.LogInformation("Changes saved successfully in EShopDbContext. Number of entities affected: {NumberOfEntities}", dbContextChanges);
//                }

//                if (_identityDbContext.ChangeTracker.HasChanges())
//                {
//                    var identityContextChanges = await _identityDbContext.SaveChangesAsync(cancellationToken);
//                    totalChanges += identityContextChanges;
//                    _logger.LogInformation("Changes saved successfully in IdentityDbContext. Number of entities affected: {NumberOfEntities}", identityContextChanges);
//                }

//                _logger.LogInformation("Total changes saved successfully. Number of entities affected: {NumberOfEntities}", totalChanges);
//                return totalChanges;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "An error occurred while saving changes.");
//                throw;
//            }
//        }

//        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
//        {
//            if (_currentTransaction != null)
//            {
//                throw new InvalidOperationException("A transaction is already in progress.");
//            }

//            try
//            {
//                _logger.LogInformation("Beginning a new transaction.");
//                _currentTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
//                await _identityDbContext.Database.UseTransactionAsync(_currentTransaction.GetDbTransaction(), cancellationToken);
//                _logger.LogInformation("Transaction started successfully for both contexts.");
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "An error occurred while starting the transaction.");
//                throw;
//            }
//        }

//        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
//        {
//            if (_currentTransaction == null)
//            {
//                throw new InvalidOperationException("No transaction is in progress.");
//            }

//            try
//            {
//                _logger.LogInformation("Committing the transaction.");
//                await _currentTransaction.CommitAsync(cancellationToken);
//                _logger.LogInformation("Transaction committed successfully for both contexts.");
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "An error occurred while committing the transaction.");
//                throw;
//            }
//            finally
//            {
//                await _currentTransaction.DisposeAsync();
//                _currentTransaction = null;
//            }
//        }

//        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
//        {
//            if (_currentTransaction == null)
//            {
//                throw new InvalidOperationException("No transaction is in progress.");
//            }

//            try
//            {
//                _logger.LogInformation("Rolling back the transaction.");
//                await _currentTransaction.RollbackAsync(cancellationToken);
//                _logger.LogInformation("Transaction rolled back successfully for both contexts.");
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "An error occurred while rolling back the transaction.");
//                throw;
//            }
//            finally
//            {
//                await _currentTransaction.DisposeAsync();
//                _currentTransaction = null;
//            }
//        }
//    }
//}
