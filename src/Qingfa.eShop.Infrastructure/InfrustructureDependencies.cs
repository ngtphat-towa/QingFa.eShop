using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using QingFa.EShop.Infrastructure.Data;

public static class InfrastructureDependencies
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Configure DbContext with the connection string
        services.AddDbContext<CatalogDbContext>(options =>
            options.UseNpgsql(connectionString)); // or UseSqlServer depending on the DB provider

        // TODO: Add logging here if needed
        // TODO: Consider manual injection if necessary

        return services;
    }
}
