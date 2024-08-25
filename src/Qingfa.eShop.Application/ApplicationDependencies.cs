using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using QingFa.EShop.Application.Core.Behaviors;
using QingFa.EShop.Application.Mappings;

namespace QingFa.EShop.Application
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(ApplicationDependencies).Assembly;

            // Register MediatR services from the assembly
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(assembly);
                // Register pipeline 
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                // Add other behaviors if needed
            });

            // Register FluentValidation validators from the assembly
            services.AddValidatorsFromAssembly(assembly);

            // Configure Mapster mappings
            MappingConfig.ConfigureMappings();

            return services;
        }
    }
}
