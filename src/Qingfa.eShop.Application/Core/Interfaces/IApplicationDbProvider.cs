using Microsoft.EntityFrameworkCore;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Core.Repositories;
using QingFa.EShop.Domain.Metas;

namespace QingFa.EShop.Application.Core.Interfaces
{
    public interface IApplicationDbProvider: IUnitOfWork
    {
        DbSet<ExampleMeta> ExampleMetas { get; }
        DbSet<Brand> Brands { get; }
        DbSet<Product> Products { get; }
        DbSet<Category> Categories { get; }
        DbSet<ProductAttributeGroup> ProductAttributeGroups { get; }
        DbSet<ProductAttributeOption> ProductAttributeOptions { get; }
        DbSet<ProductAttribute> ProductAttributes { get; }
        DbSet<ProductVariantAttribute> ProductVariantAttributes { get; set; }
    }
}
