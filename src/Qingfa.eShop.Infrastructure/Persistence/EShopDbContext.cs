using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Core.Entities;
using QingFa.EShop.Domain.Core.Repositories;
using QingFa.EShop.Domain.Metas;
using QingFa.EShop.Infrastructure.Interceptors;
using QingFa.EShop.Infrastructure.Persistence.Configurations;
using QingFa.EShop.Infrastructure.Persistence.Configurations.Attributes;

namespace QingFa.EShop.Infrastructure.Persistence
{
    internal class EShopDbContext : DbContext, IUnitOfWork, IApplicationDbContext
    {
        private readonly ILogger<EShopDbContext> _logger;
        private readonly IMediator _mediator;
        private readonly ICurrentUser _user;
        private readonly TimeProvider _dateTime;
        private IDbContextTransaction? _currentTransaction;

        public EShopDbContext(
            DbContextOptions<EShopDbContext> options,
            ILogger<EShopDbContext> logger,
            IMediator mediator,
            ICurrentUser user,
            TimeProvider dateTime)
            : base(options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
        }

        // Define DbSet properties for your entities
        public DbSet<ExampleMeta> ExampleMetas { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductAttributeGroup> ProductAttributeGroups { get; set; }
        public DbSet<ProductAttributeOption> ProductAttributeOptions { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductVariantAttribute> ProductVariantAttributes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .AddInterceptors(new DispatchDomainEventsInterceptor(_mediator))
                    .AddInterceptors(new AuditableEntityInterceptor(_user, _dateTime));
            }
        }

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
                _logger.LogInformation("Saving changes to the database.");
                var changes = await base.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Changes saved successfully. Number of entities affected: {NumberOfEntities}", changes);
                return changes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving changes.");
                throw;
            }
        }

        // Transaction management
        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }

            try
            {
                _logger.LogInformation("Beginning a new transaction.");
                _currentTransaction = await Database.BeginTransactionAsync(cancellationToken);
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

        // Optional: Log database commands
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _logger.LogInformation("Saving changes with acceptAllChangesOnSuccess: {AcceptAllChangesOnSuccess}", acceptAllChangesOnSuccess);
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override void Dispose()
        {
            _logger.LogInformation("Disposing the DbContext.");
            base.Dispose();
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
