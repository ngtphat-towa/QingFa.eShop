using Microsoft.EntityFrameworkCore;

using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Metas;

namespace QingFa.EShop.Infrastructure.Persistence
{
    internal class AppDbContext : IApplicationDbContext
    {
        private readonly EShopDbContext _dbContext;

        public AppDbContext(EShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbSet<ExampleMeta> ExampleMetas => _dbContext.Set<ExampleMeta>();
        public DbSet<Brand> Brands => _dbContext.Set<Brand>();
        public DbSet<Product> Products => _dbContext.Set<Product>();
        public DbSet<Category> Categories => _dbContext.Set<Category>();
        public DbSet<ProductAttributeGroup> ProductAttributeGroups => _dbContext.Set<ProductAttributeGroup>();
        public DbSet<ProductAttributeOption> ProductAttributeOptions => _dbContext.Set<ProductAttributeOption>();
        public DbSet<ProductAttribute> ProductAttributes => _dbContext.Set<ProductAttribute>();
    }
}
