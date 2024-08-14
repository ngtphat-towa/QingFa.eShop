using Microsoft.EntityFrameworkCore;

using QingFa.EShop.Domain.Catalogs.Brands;
using QingFa.EShop.Infrastructure.Persistence.Configurations;

namespace QingFa.EShop.Infrastructure.Persistence
{
    internal class EShopDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

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
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}
