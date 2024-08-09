using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace QingFa.eShop.Application;

public static class ApplicationDependencies
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ApplicationDependencies).Assembly;
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(assembly);
        });
        // TODO(ApplicationDependencies): better to manual injection
        // TODO(ApplicationDependencies): add logging to monitor add services
        services.AddValidatorsFromAssembly(assembly);
        return services;
    }
}