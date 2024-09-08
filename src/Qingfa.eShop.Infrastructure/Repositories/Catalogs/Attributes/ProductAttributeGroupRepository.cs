using Microsoft.EntityFrameworkCore;

using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Infrastructure.Persistence.Data;
using QingFa.EShop.Infrastructure.Repositories.Common;

namespace QingFa.EShop.Infrastructure.Repositories.Catalogs.Attributes
{
    internal class ProductAttributeGroupRepository(EShopDbContext context) 
        : GenericRepository<ProductAttributeGroup, Guid>(context), IProductAttributeGroupRepository
    {
        public async Task<bool> ExistsByNameAsync(string name,  CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .AnyAsync(c => EF.Functions.Like(c.GroupName, $"%{name}%"),cancellationToken);
        }
    }
}
