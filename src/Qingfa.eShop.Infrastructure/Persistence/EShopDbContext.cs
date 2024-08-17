using Microsoft.EntityFrameworkCore;

using QingFa.EShop.Domain.Catalogs.Aggregates;
using QingFa.EShop.Infrastructure.Persistence.Configurations.Catalogs;

namespace QingFa.EShop.Infrastructure.Persistence
{
    internal class EShopDbContext : DbContext
    {
        public DbSet<Product> CatalogItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public EShopDbContext(DbContextOptions<EShopDbContext> options) : base(options)
        {
        }

        protected EShopDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations
            modelBuilder.ApplyConfiguration(new CatalogItemConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new BrandConfiguration());


        }
    }
}
