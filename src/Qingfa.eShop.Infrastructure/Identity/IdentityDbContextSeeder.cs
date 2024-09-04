using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QingFa.EShop.Domain.Common.Enums;
using QingFa.EShop.Domain.Core.Enums;
using QingFa.EShop.Infrastructure.Identity.Entities.Permissions;
using QingFa.EShop.Infrastructure.Identity.Entities.Roles;
using QingFa.EShop.Infrastructure.Identity.Entities;
using QingFa.EShop.Domain.Catalogs.Enums;

namespace QingFa.EShop.Infrastructure.Identity
{
    public class IdentityDbContextSeeder
    {
        public static void Seed(IdentityDataDbContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                // Ensure the database is created and migrations are applied before seeding
                context.Database.Migrate();
            }

            // Check if the data already exists
            if (context.Roles.Any() || context.Permissions.Any() || context.RolePermissions.Any() || context.Users.Any())
            {
                return; // Data already exists
            }

            // Define roles
            var adminRoleId = Guid.NewGuid();
            var userRoleId = Guid.NewGuid();

            context.Roles.AddRange(
                new Role
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Created = DateTimeOffset.UtcNow,
                    Status = EntityStatus.Active,
                    Description = "Administrator role with full access."
                },
                new Role
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "USER",
                    Created = DateTimeOffset.UtcNow,
                    Status = EntityStatus.Active,
                    Description = "Regular user role with limited access."
                }
            );

            // Define permissions
            var createPermissionId = Guid.NewGuid();
            var readPermissionId = Guid.NewGuid();

            context.Permissions.AddRange(
                new Permission(
                    id: createPermissionId,
                    name: "Create",
                    permissionAction: PermissionAction.Create,
                    resourceScope: ResourceScope.Product,
                    description: "Permission to create products.",
                    created: DateTimeOffset.UtcNow,
                    lastModified: DateTimeOffset.UtcNow
                ),
                new Permission(
                    id: readPermissionId,
                    name: "Read",
                    permissionAction: PermissionAction.Read,
                    resourceScope: ResourceScope.Product,
                    description: "Permission to read products.",
                    created: DateTimeOffset.UtcNow,
                    lastModified: DateTimeOffset.UtcNow
                )
            );

            // Define role-permission mappings
            context.RolePermissions.AddRange(
                new RolePermission
                (
                    Guid.NewGuid(),
                    adminRoleId,
                    createPermissionId
                ),
                new RolePermission
                (
                    Guid.NewGuid(),
                    adminRoleId,
                    readPermissionId
                ),
                new RolePermission
                (
                    Guid.NewGuid(),
                    userRoleId,
                    readPermissionId
                )
            );

            // Optionally, seed a default user
            context.Users.Add(
                new AppUser
                {
                    Id = Guid.NewGuid(),
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<AppUser>().HashPassword(null!, "AdminPassword123!"), // Provide a non-null AppUser instance
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Created = DateTimeOffset.UtcNow,
                    Status = EntityStatus.Active
                }
            );

            context.SaveChanges();
        }
    }
}
