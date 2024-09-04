using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using QingFa.EShop.Infrastructure.Identity;
using QingFa.EShop.Infrastructure.Identity.Entities;
using QingFa.EShop.Infrastructure.Repositories.Common;

namespace QingFa.EShop.Infrastructure.Repositories.Identities.RefreshTokens
{
    internal class RefreshTokenRepository : GenericRepository<RefreshToken, Guid>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(IdentityDataDbContext context) : base(context)
        {
        }

        public async Task<RefreshToken?> GetAsync(Expression<Func<RefreshToken, bool>> predicate)
        {
            return await _context.Set<RefreshToken>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<RefreshToken>> GetAllAsync(Expression<Func<RefreshToken, bool>> predicate)
        {
            return await _context.Set<RefreshToken>().Where(predicate).ToListAsync();
        }
    }
}