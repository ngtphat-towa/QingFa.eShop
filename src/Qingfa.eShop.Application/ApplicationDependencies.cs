using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using QingFa.EShop.Application.Core.Behaviors;
using QingFa.EShop.Application.Mappings;

namespace QingFa.EShop.Application;

public static class ApplicationDependencies
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ApplicationDependencies).Assembly;

        // Register MediatR services from the assembly
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(assembly);
            // Register pipeline behaviors
            //config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            //config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            //config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
        });

        // Register FluentValidation validators from the assembly
        services.AddValidatorsFromAssembly(assembly);

        //services.AddScoped<IIdentityService, IdentityService>(); // Adjust with your concrete implementation

        // Configure Mapster mappings
        MappingConfig.ConfigureMappings();

        return services;
    }
}