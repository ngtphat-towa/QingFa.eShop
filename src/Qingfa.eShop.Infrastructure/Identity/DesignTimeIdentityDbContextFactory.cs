using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace QingFa.EShop.Infrastructure.Identity
{
    internal class DesignTimeIdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityDataDbContext>
    {
        public IdentityDataDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var optionsBuilder = new DbContextOptionsBuilder<IdentityDataDbContext>();
            optionsBuilder.UseSqlite(configuration.GetConnectionString("IdentityConnection"));

            return new IdentityDataDbContext(optionsBuilder.Options, null!, null!, null!, null!);
        }

        private IConfiguration BuildConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return configurationBuilder.Build();
        }
    }
}
