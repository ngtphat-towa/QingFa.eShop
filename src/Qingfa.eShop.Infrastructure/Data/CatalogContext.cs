using Microsoft.EntityFrameworkCore;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Infrastructure.Data.Configurations;

namespace QingFa.EShop.Infrastructure.Data
{
    public class CatalogContext : DbContext
    {
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogCategory> CatalogCategories { get; set; }
        public DbSet<CatalogSubCategory> CatalogSubCategories { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<AgeGroup> AgeGroups { get; set; } // Added AgeGroup DbSet

        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations from separate classes
            modelBuilder.ApplyConfiguration(new CatalogBrandConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogSubCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GenderConfiguration());
            modelBuilder.ApplyConfiguration(new ColorConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialConfiguration());
            modelBuilder.ApplyConfiguration(new SizeConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new AgeGroupConfiguration());
        }
    }
}
