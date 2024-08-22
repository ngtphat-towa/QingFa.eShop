using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Core.Repositories;
using QingFa.EShop.Domain.Metas;
using QingFa.EShop.Infrastructure.Persistence.Configurations;

namespace QingFa.EShop.Infrastructure.Persistence
{
    public class EShopDbContext : DbContext, IUnitOfWork
    {
        public EShopDbContext(DbContextOptions<EShopDbContext> options)
            : base(options)
        {
        }

        // Define DbSet properties for your entities
        public DbSet<ExampleMeta> ExampleMetas { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryProductConfiguration());
            modelBuilder.ApplyConfiguration(new BrandConfiguration());
        }

        // IUnitOfWork implementation
        public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Optional: Add any additional logic here if needed
            return await base.SaveChangesAsync(cancellationToken);
        }

        // Transaction management
        private IDbContextTransaction? _currentTransaction;

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }

            _currentTransaction = await Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction == null)
            {
                throw new InvalidOperationException("No transaction is in progress.");
            }

            await _currentTransaction.CommitAsync(cancellationToken);
            _currentTransaction.Dispose();
            _currentTransaction = null;
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction == null)
            {
                throw new InvalidOperationException("No transaction is in progress.");
            }

            await _currentTransaction.RollbackAsync(cancellationToken);
            _currentTransaction.Dispose();
            _currentTransaction = null;
        }
    }
}
