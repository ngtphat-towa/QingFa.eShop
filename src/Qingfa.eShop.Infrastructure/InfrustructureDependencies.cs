using Microsoft.Extensions.DependencyInjection;

namespace QingFa.eShop.Infrastructure;

public static class InfrastructureDependencies
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {

        // TODO(InfrastructureDependencies): better to manual injection
        // TODO(InfrastructureDependencies): add logging to monitor add services

        return services;
    }
}