using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using QingFa.EShop.Infrastructure.Identity;
using QingFa.EShop.Infrastructure.Persistence;

namespace QingFa.EShop.Infrastructure
{
    public static class InfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Add identity services
            services.AddIdentityServices(configuration);

            // Add persistence services
            services.AddPersistence(configuration);

            return services;
        }
    }
}
