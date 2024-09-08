using QingFa.EShop.Domain.Core.Repositories;
using QingFa.EShop.Domain.Identities.Entities;

namespace QingFa.EShop.Infrastructure.Repositories.Identities.Permissions
{
    public interface IPermissionRepository : IGenericRepository<Permission,Guid>
    {
        Task AddAsync(Permission entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(Permission entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(Permission entity, CancellationToken cancellationToken = default);
    }
}
