using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using QingFa.EShop.Domain.DomainModels.Interfaces;
using QingFa.EShop.Domain.DomainModels.Repositories;
using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly Dictionary<Type, object> _repositories = new();
        private IDbContextTransaction? _transaction;

        public UnitOfWork(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IGenericRepository<TEntity, TId> Repository<TEntity, TId>()
            where TEntity : Entity<TId>
            where TId : notnull
        {
            if (_repositories.TryGetValue(typeof(TEntity), out var repository))
            {
                return (IGenericRepository<TEntity, TId>)repository;
            }

            var repositoryType = typeof(GenericRepository<,>).MakeGenericType(typeof(TEntity), typeof(TId));
            var repositoryInstance = Activator.CreateInstance(repositoryType, _context);

            if (repositoryInstance == null)
            {
                throw new InvalidOperationException($"Unable to create repository instance for type {typeof(TEntity).FullName}");
            }

            _repositories.Add(typeof(TEntity), repositoryInstance);
            return (IGenericRepository<TEntity, TId>)repositoryInstance;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                if (_transaction != null)
                {
                    await _transaction.CommitAsync(cancellationToken);
                    _transaction.Dispose();
                    _transaction = null;
                }
                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                if (_transaction != null)
                {
                    await _transaction.RollbackAsync(cancellationToken);
                    _transaction.Dispose();
                    _transaction = null;
                }
                throw;
            }
        }

        public Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction = _context.Database.BeginTransaction();
            return Task.CompletedTask;
        }

        public Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
                _transaction = null;
            }
            return Task.CompletedTask;
        }
    }
}
