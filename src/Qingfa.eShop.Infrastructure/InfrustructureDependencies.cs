using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QingFa.EShop.Infrastructure.Repositories;
using QingFa.EShop.Domain.Core.Repositories;
using QingFa.EShop.Infrastructure.Persistence;
using QingFa.EShop.Domain.Metas;

namespace QingFa.EShop.Infrastructure
{
    public static class InfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Retrieve the connection string from configuration
            var connectionString = configuration.GetConnectionString("SqliteConnection");

            // Register DbContext with the connection string
            services.AddDbContext<EShopDbContext>(options =>
                options.UseSqlite(connectionString));

            // Register repositories
            services.AddScoped<IGenericRepository<ExampleMeta, Guid>, ExampleMetaRepository>();

            // Register unit of work
            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<EShopDbContext>());

            // Optional: Add logging for debugging and monitoring
            services.AddLogging();

            return services;
        }
    }
}
