using Microsoft.EntityFrameworkCore;

using QingFa.EShop.Domain.Catalogs.Attributes;
using QingFa.EShop.Domain.Catalogs.Brands;
using QingFa.EShop.Domain.Catalogs.Categories;
using QingFa.EShop.Domain.Catalogs.Products;
using QingFa.EShop.Domain.Catalogs.Variants;
using QingFa.EShop.Infrastructure.Persistence.Configurations.Catalogs;

namespace QingFa.EShop.Infrastructure.Persistence
{
    public class EShopDbContext : DbContext
    {
        public EShopDbContext(DbContextOptions<EShopDbContext> options)
            : base(options)
        {
        }

        // DbSets for your entities
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Domain.Catalogs.Attributes.ProductAttribute> VariantAttributes { get; set; }
        public DbSet<AttributeOption> AttributeOptions { get; set; }
        public DbSet<AttributeGroup> AttributeGroups { get; set; }

        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductVariantAttribute> ProductVariantAttributes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Apply entity configurations
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new BrandConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            modelBuilder.ApplyConfiguration(new AttributeOptionConfiguration());
            modelBuilder.ApplyConfiguration(new AttributeConfiguration());
            modelBuilder.ApplyConfiguration(new AttributeGroupConfiguration());

            modelBuilder.ApplyConfiguration(new ProductVariantConfiguration());
            modelBuilder.ApplyConfiguration(new ProductVariantAttributeConfiguration());


            base.OnModelCreating(modelBuilder);

        }
    }
}
