using APIGatewayCoreUtilities.CommonConfiguration.ConfigurationModels;
using AspNetCoreRateLimit;
using Microsoft.OpenApi.Models;

namespace APIGatewayMain.ServiceCollectionExtensions
{
    internal static class ConfigurationExtensions
    {
        public static IServiceCollection AddCommonConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));

            services.Configure<JwtSettings>(configuration.GetSection("AuthorizationSettings:JwtSettings"));
            services.Configure<AuthorizationServiceApiSettings>(configuration.GetSection("ComponentsSettings:AuthorizationServiceApiSettings"));
            services.Configure<StreamServiceApiSettings>(configuration.GetSection("ComponentsSettings:StreamServiceApiSettings"));
            services.Configure<LicenseServiceClientSettings>(configuration.GetSection("ComponentsSettings:LicenseServiceClientSettings"));
            services.Configure<StreamGatewayApiSettings>(configuration.GetSection("ComponentsSettings:StreamGatewayApiSettings"));

            return services;
        }

        public static WebApplicationBuilder AddKestrelSettings(this WebApplicationBuilder builder, KestrelSettings kestrelSettings)
        {
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.Listen(System.Net.IPAddress.Parse(kestrelSettings.ListeningIPv4Address), kestrelSettings.PortNumber);

                if (kestrelSettings.UseTls)
                {
                    serverOptions.Listen(System.Net.IPAddress.Parse(kestrelSettings.ListeningIPv4Address), kestrelSettings.TlsPortNumber, listenOptions =>
                    {
                        listenOptions.UseHttps();
                    });
                }
            });

            return builder;
        }

        public static IServiceCollection AddSwaggerSettings(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIGateway API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Enter JWT token with format: {Bearer <token>}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }
    }
}
