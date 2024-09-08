using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Application.Features.CategoryManagements.Services;
using QingFa.EShop.Domain.Catalogs.Repositories.Attributes;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Domain.Core.Repositories;
using QingFa.EShop.Domain.Metas;
using QingFa.EShop.Infrastructure.Persistence.Data;
using QingFa.EShop.Infrastructure.Repositories.Catalogs.Attributes;
using QingFa.EShop.Infrastructure.Repositories.Catalogs;
using QingFa.EShop.Infrastructure.Repositories.Common;
using QingFa.EShop.Infrastructure.Repositories;
using QingFa.EShop.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

internal static class PersistenceDependencies
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = GetConnectionString(configuration);

        services.AddDbContext<EShopDbContext>(options =>
        {
            options.UseSqlite(connectionString).EnableSensitiveDataLogging();
        });

        RegisterRepositories(services);
        RegisterThirdPartyServices(services);

        return services;
    }

    private static string GetConnectionString(IConfiguration configuration)
    {
        return configuration.GetConnectionString("DefaultConnection")!;
    }

    private static void RegisterRepositories(IServiceCollection services)
    {
        // Register IApplicationDbProvider to use EShopDbContext
        services.AddScoped<IApplicationDbProvider, EShopDbContext>();

        // Register repositories
        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        services.AddScoped<IExampleMetaRepository, ExampleMetaRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryService, CategoryService>();

        services.AddScoped<IProductAttributeGroupRepository, ProductAttributeGroupRepository>();
        services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
        services.AddScoped<IProductAttributeOptionRepository, ProductAttributeOptionRepository>();
    }

    private static void RegisterThirdPartyServices(IServiceCollection services)
    {
        // Register third-party services
        services.AddScoped<IEmailService, EmailService>();

        services.AddDistributedMemoryCache();
    }
}
