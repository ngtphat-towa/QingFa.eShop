using Microsoft.EntityFrameworkCore;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Infrastructure.Configurations;
using QingFa.EShop.Infrastructure.Persistence.Configurations;

namespace QingFa.EShop.Infrastructure.Persistence
{
    public class EShopDbContext : DbContext
    {
        public EShopDbContext(DbContextOptions<EShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<SizeOption> SizeOptions { get; set; }
        public DbSet<ArticleType> ArticleTypes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ImageData> ImageDatas { get; set; }
        public DbSet<StyleImage> StyleImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations
            modelBuilder.ApplyConfiguration(new ArticleTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogBrandConfiguration());
            modelBuilder.ApplyConfiguration(new SizeOptionConfiguration());
            modelBuilder.ApplyConfiguration(new ImageDataConfiguration());
            modelBuilder.ApplyConfiguration(new StyleImageConfiguration());
        }
    }
}
