using Microsoft.OpenApi.Models;

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

            // Add controllers
            services.AddControllers();

            services.AddScoped<ICurrentUser, CurrentUser>();

            services.AddHttpContextAccessor();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QingFa eShop API", Version = "v1" });

                // Add JWT Bearer token support in Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            return services;
        }
    }
}
