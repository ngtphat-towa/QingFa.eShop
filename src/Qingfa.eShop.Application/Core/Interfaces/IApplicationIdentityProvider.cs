using Microsoft.EntityFrameworkCore;
using QingFa.EShop.Domain.Identities.Entities;
using QingFa.EShop.Infrastructure.Identity.Entities.Roles;
using QingFa.EShop.Infrastructure.Identity.Entities;
using QingFa.EShop.Domain.Core.Repositories;

namespace QingFa.EShop.Application.Core.Interfaces
{
    public interface IApplicationIdentityProvider : IUnitOfWork
    {
        DbSet<AppUser> Users { get; }
        DbSet<Role> Roles { get; }
        DbSet<RolePermission> RolePermissions { get; }
        DbSet<Permission> Permissions { get; }
        DbSet<RefreshToken> RefreshTokens { get; }
    }
}
