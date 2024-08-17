﻿using Microsoft.EntityFrameworkCore;

using QingFa.EShop.Domain.Catalogs.Brands;
using QingFa.EShop.Domain.Catalogs.Categories;
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
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply entity configurations
            modelBuilder.ApplyConfiguration(new BrandConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}
