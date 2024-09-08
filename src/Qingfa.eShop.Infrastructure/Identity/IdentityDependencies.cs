using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using QingFa.EShop.Infrastructure.Identity.Entities.Roles;
using QingFa.EShop.Infrastructure.Identity.Settings;
using QingFa.EShop.Infrastructure.Identity.Services.Tokens;
using QingFa.EShop.Application.Features.AccountManagements.Services;
using QingFa.EShop.Infrastructure.Identity.Services.RefreshTokens;
using QingFa.EShop.Infrastructure.Repositories.Identities.RefreshTokens;
using QingFa.EShop.Infrastructure.Identity.Services.Accounts;
using QingFa.EShop.Infrastructure.Identity.Services.Emails;
using System.Net.Mail;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QingFa.EShop.Domain.Core.Enums;
using QingFa.EShop.Domain.Identities.Entities;

namespace QingFa.EShop.Infrastructure.Identity
{
    internal static class IdentityDependencies
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureEmailService(services, configuration);
            ConfigureDatabase(services, configuration);
            ConfigureIdentity(services);
            ConfigureJwtAuthentication(services, configuration);

            services.Configure<DataProtectionTokenProviderOptions>(opts => opts.TokenLifespan = TimeSpan.FromHours(24));

            return services;
        }

        private static void ConfigureEmailService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentEmail("no-reply@qingfa-shop.com")
                .AddSmtpSender(new SmtpClient
                {
                    Host = configuration["Smtp:Host"]!,
                    Port = configuration.GetValue<int>("Smtp:Port"),
                    EnableSsl = configuration.GetValue<bool>("Smtp:EnableSsl")
                });

            services.AddTransient<IEmailSender, FluentEmailSender>();
        }

        private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("IdentityConnection");
            services.AddDbContext<IdentityDataDbContext>(options => options.UseSqlite(connectionString));
        }

        private static void ConfigureIdentity(IServiceCollection services)
        {
            services.AddIdentity<AppUser, Role>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
            .AddEntityFrameworkStores<IdentityDataDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        }

        private static void ConfigureJwtAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.GetSection("JwtSettings").Bind(jwtSettings);

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => ConfigureJwtBearerOptions(options, jwtSettings));
        }

        private static void ConfigureJwtBearerOptions(JwtBearerOptions options, JwtSettings jwtSettings)
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                ClockSkew = TimeSpan.Zero
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    return WriteProblemDetailsResponse(context.Response, StatusCodes.Status401Unauthorized,
                        "Authentication Failed", "An error occurred while processing your authentication.");
                },

                OnChallenge = context =>
                {
                    context.HandleResponse();
                    return WriteProblemDetailsResponse(context.Response, StatusCodes.Status401Unauthorized,
                        "Unauthorized", "You are not authorized to access this resource.");
                },

                OnForbidden = context =>
                {
                    return WriteProblemDetailsResponse(context.Response, StatusCodes.Status403Forbidden,
                        "Forbidden", "You are forbidden to access this resource.");
                },

                OnTokenValidated = async context =>
                {
                    var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<AppUser>>();
                    var userId = context.Principal?.FindFirstValue(ClaimTypes.NameIdentifier);

                    if (userId != null)
                    {
                        var user = await userManager.FindByIdAsync(userId);
                        if (user == null || user.Status != EntityStatus.Active)
                        {
                            context.Fail("User not found or inactive.");
                        }
                    }
                },

                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    var path = context.HttpContext.Request.Path;

                    if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                    {
                        context.Token = accessToken;
                    }

                    return Task.CompletedTask;
                }
            };
        }

        private static Task WriteProblemDetailsResponse(HttpResponse response, int statusCode, string title, string detail)
        {
            response.StatusCode = statusCode;
            response.ContentType = "application/problem+json";

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Instance = response.HttpContext.Request.Path
            };

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            return response.WriteAsync(JsonSerializer.Serialize(problemDetails, options));
        }
    }
}
