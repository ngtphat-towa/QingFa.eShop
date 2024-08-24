using Microsoft.EntityFrameworkCore;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Metas;

namespace QingFa.EShop.Application.Core.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<ExampleMeta> ExampleMetas { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
