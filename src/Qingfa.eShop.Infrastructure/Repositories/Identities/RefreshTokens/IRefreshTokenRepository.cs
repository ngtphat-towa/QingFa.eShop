using System.Linq.Expressions;

using QingFa.EShop.Domain.Core.Repositories;
using QingFa.EShop.Infrastructure.Identity.Entities;

namespace QingFa.EShop.Infrastructure.Repositories.Identities.RefreshTokens
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken, Guid>
    {
        Task<RefreshToken?> GetAsync(Expression<Func<RefreshToken, bool>> predicate);
        Task<IEnumerable<RefreshToken>> GetAllAsync(Expression<Func<RefreshToken, bool>> predicate);
    }
}