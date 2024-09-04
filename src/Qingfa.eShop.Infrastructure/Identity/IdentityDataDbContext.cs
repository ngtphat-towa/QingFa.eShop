using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MediatR;
using QingFa.EShop.Infrastructure.Identity.Entities.Roles;
using QingFa.EShop.Infrastructure.Identity.Entities;
using QingFa.EShop.Infrastructure.Interceptors;
using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Infrastructure.Identity.Configurations;
using QingFa.EShop.Infrastructure.Identity.Entities.Permissions;

namespace QingFa.EShop.Infrastructure.Identity
{
    public class IdentityDataDbContext : IdentityDbContext<AppUser, Role, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        private readonly ILogger<IdentityDataDbContext>? _logger;
        private readonly IMediator? _mediator;
        private readonly ICurrentUser? _user;
        private readonly TimeProvider? _dateTime;
        public IdentityDataDbContext(DbContextOptions<IdentityDataDbContext> options)
      : base(options)
        {
        }

        public IdentityDataDbContext(
            DbContextOptions<IdentityDataDbContext> options,
            ILogger<IdentityDataDbContext>? logger = null,
            IMediator? mediator = null,
            ICurrentUser? user = null,
            TimeProvider? dateTime = null)
            : base(options)
        {
            _logger = logger;
            _mediator = mediator;
            _user = user;
            _dateTime = dateTime;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .AddInterceptors(new DispatchDomainEventsInterceptor(_mediator!))
                    .AddInterceptors(new AuditableEntityInterceptor(_user!, _dateTime!));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations for identity entities
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        }

        // DbSets for the entities
        public DbSet<RolePermission> RolePermissions { get; set; } = default!;
        public DbSet<Permission> Permissions { get; set; } = default!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;
    }
}
