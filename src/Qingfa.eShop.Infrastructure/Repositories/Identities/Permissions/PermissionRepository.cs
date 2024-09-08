using QingFa.EShop.Domain.Identities.Entities;
using QingFa.EShop.Infrastructure.Identity;
using QingFa.EShop.Infrastructure.Repositories.Common;

namespace QingFa.EShop.Infrastructure.Repositories.Identities.Permissions
{
    internal class PermissionRepository : GenericRepository<Permission, Guid>, IPermissionRepository
    {
        public PermissionRepository(IdentityDataDbContext context) : base(context)
        {
        }

        public new async Task AddAsync(Permission entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _context.Set<Permission>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public new async Task UpdateAsync(Permission entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var existingEntity = await _context.Set<Permission>().FindAsync(new object[] { entity.Id }, cancellationToken);
            if (existingEntity == null)
            {
                throw new InvalidOperationException("Permission does not exist.");
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public new async Task DeleteAsync(Permission entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var existingEntity = await _context.Set<Permission>().FindAsync(new object[] { entity.Id }, cancellationToken);
            if (existingEntity == null)
            {
                throw new InvalidOperationException("Permission does not exist.");
            }

            _context.Set<Permission>().Remove(existingEntity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
