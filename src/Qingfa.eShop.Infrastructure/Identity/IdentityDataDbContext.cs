using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MediatR;
using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Domain.Identities.Entities;
using QingFa.EShop.Infrastructure.Identity.Configurations;
using QingFa.EShop.Infrastructure.Interceptors;
using QingFa.EShop.Infrastructure.Identity.Entities.Roles;
using QingFa.EShop.Infrastructure.Identity.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace QingFa.EShop.Infrastructure.Identity
{
    public class IdentityDataDbContext : IdentityDbContext<AppUser, Role, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>, IApplicationIdentityProvider
    {
        private readonly ILogger<IdentityDataDbContext>? _logger;
        private readonly IMediator? _mediator;
        private readonly ICurrentUser? _user;
        private readonly TimeProvider? _dateTime;
        private IDbContextTransaction? _currentTransaction;

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

        public DbSet<RolePermission> RolePermissions { get; set; } = default!;
        public DbSet<Permission> Permissions { get; set; } = default!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }

            try
            {
                _logger?.LogInformation("Beginning a new transaction.");
                _currentTransaction = await Database.BeginTransactionAsync(cancellationToken);
                _logger?.LogInformation("Transaction started successfully.");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An error occurred while starting the transaction.");
                throw;
            }
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction == null)
            {
                throw new InvalidOperationException("No transaction is in progress.");
            }

            try
            {
                _logger?.LogInformation("Committing the transaction.");
                await _currentTransaction.CommitAsync(cancellationToken);
                _logger?.LogInformation("Transaction committed successfully.");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An error occurred while committing the transaction.");
                throw;
            }
            finally
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction == null)
            {
                throw new InvalidOperationException("No transaction is in progress.");
            }

            try
            {
                _logger?.LogInformation("Rolling back the transaction.");
                await _currentTransaction.RollbackAsync(cancellationToken);
                _logger?.LogInformation("Transaction rolled back successfully.");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An error occurred while rolling back the transaction.");
                throw;
            }
            finally
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                if (_mediator != null)
                {
                    optionsBuilder.AddInterceptors(new DispatchDomainEventsInterceptor(_mediator));
                }

                if (_user != null && _dateTime != null)
                {
                    optionsBuilder.AddInterceptors(new AuditableEntityInterceptor(_user, _dateTime));
                }
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

            // Add any additional configuration if needed
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                _logger?.LogInformation("Saving changes to the database.");
                int totalChanges = 0;

                if (ChangeTracker.HasChanges())
                {
                    var dbContextChanges = await base.SaveChangesAsync(cancellationToken);
                    totalChanges += dbContextChanges;
                    _logger?.LogInformation("Changes saved successfully. Number of entities affected: {NumberOfEntities}", dbContextChanges);
                }

                _logger?.LogInformation("Total changes saved successfully. Number of entities affected: {NumberOfEntities}", totalChanges);
                return totalChanges;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An error occurred while saving changes.");
                throw;
            }
        }
    }
}
