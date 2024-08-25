using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

using QingFa.EShop.Api.Filters;
using QingFa.EShop.Api.OptionsSetup;
using QingFa.EShop.Api.Services;
using QingFa.EShop.Application.Core.Interfaces;

namespace QingFa.EShop.Api
{
    public static class PresentationDependencies
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.ConfigureOptions<JwtOptionsSetup>();
            services.ConfigureOptions<JwtBearerOptionsSetup>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            // Add controllers
            services.AddControllers();

            services.AddScoped<ICurrentUser, CurrentUser>();

            services.AddHttpContextAccessor();

            // Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QingFa eShop API", Version = "v1" });

                // Enable annotations if you are using attributes in your controller actions
                c.EnableAnnotations();
                c.SchemaFilter<EnumSchemaFilter>();

            });
            return services;
        }
    }
}
