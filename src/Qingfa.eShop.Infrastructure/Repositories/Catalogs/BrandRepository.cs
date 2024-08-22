using Microsoft.EntityFrameworkCore;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Infrastructure.Persistence;

namespace QingFa.EShop.Infrastructure.Repositories.Catalogs
{
    public class BrandRepository : GenericRepository<Brand, Guid>, IBrandRepository
    {
        private readonly DbSet<Brand> _brandSet;

        public BrandRepository(EShopDbContext context) : base(context)
        {
            _brandSet = context.Set<Brand>();
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _brandSet
                .AnyAsync(b => EF.Functions.Like(b.Name, name), cancellationToken);
        }

    }
}