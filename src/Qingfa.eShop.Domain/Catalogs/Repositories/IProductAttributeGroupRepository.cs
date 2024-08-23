using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Core.Repositories;

namespace QingFa.EShop.Domain.Catalogs.Repositories
{
    public interface IProductAttributeGroupRepository : IGenericRepository<ProductAttributeGroup, Guid>
    {
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
