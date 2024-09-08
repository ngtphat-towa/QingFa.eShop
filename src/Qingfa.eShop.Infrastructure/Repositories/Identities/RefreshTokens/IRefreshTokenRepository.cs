using System.Linq.Expressions;

using QingFa.EShop.Domain.Core.Repositories;
using QingFa.EShop.Domain.Identities.Entities;
using QingFa.EShop.Infrastructure.Identity.Entities;

namespace QingFa.EShop.Infrastructure.Repositories.Identities.RefreshTokens
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken, Guid>
    {
        Task<RefreshToken?> GetAsync(Expression<Func<RefreshToken, bool>> predicate);
        Task<IEnumerable<RefreshToken>> GetAllAsync(Expression<Func<RefreshToken, bool>> predicate);
        Task AddAsync(RefreshToken entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(RefreshToken entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(RefreshToken entity, CancellationToken cancellationToken = default);
    }
}