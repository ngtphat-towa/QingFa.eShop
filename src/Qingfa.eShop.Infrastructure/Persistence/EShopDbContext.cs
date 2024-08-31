using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Core.Entities;
using QingFa.EShop.Domain.Metas;
using QingFa.EShop.Infrastructure.Identity.Entities;
using QingFa.EShop.Infrastructure.Identity.Entities.Roles;
using QingFa.EShop.Infrastructure.Interceptors;
using QingFa.EShop.Infrastructure.Persistence.Configurations.Attributes;
using QingFa.EShop.Infrastructure.Persistence.Configurations.Identities;
using QingFa.EShop.Infrastructure.Persistence.Configurations;

namespace QingFa.EShop.Infrastructure.Persistence
{
    internal class EShopDbContext : IdentityDbContext<AppUser, Role, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        private readonly ILogger<EShopDbContext> _logger;
        private readonly IMediator _mediator;
        private readonly ICurrentUser _user;
        private readonly TimeProvider _dateTime;

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

            // Apply configurations for identity entities
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());

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
