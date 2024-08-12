using Microsoft.EntityFrameworkCore;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs;
using QingFa.EShop.Infrastructure.Persistence.Configurations;

namespace QingFa.EShop.Infrastructure.Data
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
            : base(options)
        {
        }

        public DbSet<CatalogProduct> CatalogProducts { get; set; }
        public DbSet<CatalogCategory> CatalogCategories { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }
        public DbSet<AdditionalCost> AdditionalCosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations
            modelBuilder.ApplyConfiguration(new CatalogProductConfigurations());
            modelBuilder.ApplyConfiguration(new CatalogCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogBrandConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AdditionalCostConfiguration());
        }
    }
}
