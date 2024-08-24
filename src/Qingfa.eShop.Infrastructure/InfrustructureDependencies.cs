using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QingFa.EShop.Infrastructure.Repositories;
using QingFa.EShop.Domain.Core.Repositories;
using QingFa.EShop.Infrastructure.Persistence;
using QingFa.EShop.Domain.Metas;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Infrastructure.Repositories.Catalogs;
using QingFa.EShop.Application.Features.CategoryManagements.Services;
using QingFa.EShop.Infrastructure.Services;
using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Infrastructure.Repositories.Catalogs.Attributes;
using QingFa.EShop.Domain.Catalogs.Repositories.Attributes;

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
            {
                options.UseSqlite(connectionString).EnableSensitiveDataLogging();
            });

            // Register IApplicationDbContext to use EShopDbContext
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<EShopDbContext>());

            // Register repositories
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped<IExampleMetaRepository, ExampleMetaRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IProductAttributeGroupRepository, ProductAttributeGroupRepository>();
            services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
            services.AddScoped<IProductAttributeOptionRepository, ProductAttributeOptionRepository>();

            // Register unit of work
            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<EShopDbContext>());

            // Optional: Add logging for debugging and monitoring
            services.AddLogging();

            return services;
        }
    }
}
