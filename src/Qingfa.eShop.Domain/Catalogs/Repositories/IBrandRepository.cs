using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Core.Repositories;

namespace QingFa.EShop.Domain.Catalogs.Repositories
{
    public interface IBrandRepository : IGenericRepository<Brand, Guid>
    {
        public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
