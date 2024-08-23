using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Core.Entities;
using QingFa.EShop.Domain.Core.Repositories;
using QingFa.EShop.Domain.Metas;
using QingFa.EShop.Infrastructure.Persistence.Configurations;
using QingFa.EShop.Infrastructure.Persistence.Configurations.Attributes;

namespace QingFa.EShop.Infrastructure.Persistence
{
    public class EShopDbContext : DbContext, IUnitOfWork
    {
        private readonly ILogger<EShopDbContext> _logger;
        private IDbContextTransaction? _currentTransaction;

        public EShopDbContext(DbContextOptions<EShopDbContext> options, ILogger<EShopDbContext> logger)
            : base(options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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

            modelBuilder.ApplyConfiguration(new ProductAttributeGroupConfiguration());
            modelBuilder.ApplyConfiguration(new ProductAttributeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductVariantConfiguration());
            modelBuilder.ApplyConfiguration(new ProductVariantAttributeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductAttributeOptionConfiguration());

            // Apply index configurations for audit entities
            ConfigureIndexesForAuditEntities(modelBuilder);
        }

        private void ConfigureIndexesForAuditEntities(ModelBuilder modelBuilder)
        {
            var auditEntityTypes = modelBuilder.Model.GetEntityTypes()
                .Where(e => typeof(AuditEntity).IsAssignableFrom(e.ClrType))
                .ToList();

            foreach (var entityType in auditEntityTypes)
            {
                var entityBuilder = modelBuilder.Entity(entityType.ClrType);
                var tableName = entityType.GetTableName() ?? entityType.ClrType.Name;

                foreach (var property in entityType.GetProperties())
                {
                    if (IsAuditProperty(property.Name))
                    {
                        entityBuilder.HasIndex(property.Name)
                            .HasDatabaseName(EntityTypeHelper.CreateIndexName(tableName, property.Name));
                    }
                }
            }
        }

        private bool IsAuditProperty(string propertyName)
        {
            // List of known audit properties
            var auditProperties = new[]
            {
                "Created",
                "LastModified",
                "Status",
                "CreatedBy",
                "LastModifiedBy"
            };

            return auditProperties.Contains(propertyName);
        }

        // IUnitOfWork implementation
        public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving changes.");
                throw; // Rethrow the exception after logging it
            }
        }

        // Transaction management
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

            try
            {
                await _currentTransaction.CommitAsync(cancellationToken);
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
                await _currentTransaction.RollbackAsync(cancellationToken);
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

    public static class EntityTypeHelper
    {
        public static IEnumerable<Type> GetDerivedEntityTypes(IModel model, Type baseType)
        {
            return model.GetEntityTypes()
                .Where(e => baseType.IsAssignableFrom(e.ClrType))
                .Select(e => e.ClrType)
                .ToList();
        }

        public static string CreateIndexName(string tableName, string columnName)
        {
            return $"IX_{tableName}_{columnName}";
        }

        public static string? GetTableName(IEntityType entityType)
        {
            return entityType.GetTableName();
        }
    }
}
