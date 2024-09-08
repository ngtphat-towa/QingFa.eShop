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

        public new async Task AddAsync(RefreshToken entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _context.Set<RefreshToken>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public new async Task UpdateAsync(RefreshToken entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var existingEntity = await _context.Set<RefreshToken>().FindAsync(new object[] { entity.Id }, cancellationToken);
            if (existingEntity == null)
            {
                throw new InvalidOperationException("RefreshToken does not exist.");
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public new async Task DeleteAsync(RefreshToken entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var existingEntity = await _context.Set<RefreshToken>().FindAsync(new object[] { entity.Id }, cancellationToken);
            if (existingEntity == null)
            {
                throw new InvalidOperationException("RefreshToken does not exist.");
            }

            _context.Set<RefreshToken>().Remove(existingEntity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
