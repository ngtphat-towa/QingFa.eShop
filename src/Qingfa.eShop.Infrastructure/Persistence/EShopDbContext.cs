using Microsoft.EntityFrameworkCore;

using QingFa.EShop.Domain.Core.Repositories;
using QingFa.EShop.Domain.Metas;

namespace QingFa.EShop.Infrastructure.Persistence
{
    public class EShopDbContext : DbContext, IUnitOfWork
    {
        public EShopDbContext(DbContextOptions<EShopDbContext> options)
            : base(options)
        {
        }

        // Define DbSet properties for your entities
        public DbSet<ExampleMeta> ExampleMetas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure your entities here
            // Example: modelBuilder.Entity<ExampleMeta>().ToTable("ExampleMetas");
        }

        // IUnitOfWork implementation
        public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Optional: Add any additional logic here if needed
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
