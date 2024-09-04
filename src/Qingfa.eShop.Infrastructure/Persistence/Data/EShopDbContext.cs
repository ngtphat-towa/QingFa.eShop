using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MediatR;
using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Core.Entities;
using QingFa.EShop.Domain.Metas;
using QingFa.EShop.Infrastructure.Interceptors;
using QingFa.EShop.Infrastructure.Persistence.Configurations.Attributes;
using QingFa.EShop.Infrastructure.Persistence.Configurations;

namespace QingFa.EShop.Infrastructure.Persistence.Data
{
    public class EShopDbContext : DbContext
    {
        private readonly ILogger<EShopDbContext>? _logger;
        private readonly IMediator? _mediator;
        private readonly ICurrentUser? _user;
        private readonly TimeProvider? _dateTime;

        public EShopDbContext(
            DbContextOptions<EShopDbContext> options,
            ILogger<EShopDbContext>? logger = null,
            IMediator? mediator = null,
            ICurrentUser? user = null,
            TimeProvider? dateTime = null)
            : base(options)
        {
            _logger = logger;
            _mediator = mediator;
            _user = user;
            _dateTime = dateTime;
        }

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
            if (!optionsBuilder.IsConfigured)
            {
                // Null checks for dependencies
                if (_mediator != null)
                {
                    optionsBuilder.AddInterceptors(new DispatchDomainEventsInterceptor(_mediator));
                }

                if (_user != null && _dateTime != null)
                {
                    optionsBuilder.AddInterceptors(new AuditableEntityInterceptor(_user, _dateTime));
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations for catalog entities
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
    }
}
