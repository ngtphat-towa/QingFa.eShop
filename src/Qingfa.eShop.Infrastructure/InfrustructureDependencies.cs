using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using QingFa.EShop.Infrastructure.Persistence;

namespace QingFa.EShop.Infrastructure
{
    public static class InfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var useSqliteString = configuration.GetSection("UseSqlite").Value;
            bool useSqlite = bool.TryParse(useSqliteString, out var result) ? result : false;

            if (useSqlite)
            {
                services.AddDbContext<EShopDbContext>(options =>
                    options.UseSqlite(configuration.GetConnectionString("SqliteConnection"))
                           .EnableSensitiveDataLogging());
            }
            else
            {
                services.AddDbContext<EShopDbContext>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                           .EnableSensitiveDataLogging());
            }

            return services;
        }
    }
}
