using APIGatewayCoreUtilities.CommonConfiguration.ConfigurationModels.MockSettings;
using APIGatewayCoreUtilities.CommonConfiguration.ConfigurationModels;

namespace APIGatewayControllers.ServiceCollectionExtensions
{
    internal static class ConfigurationExtensions
    {
        public static IServiceCollection AddCommonConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("AuthorizationSettings:JwtSettings"));
            services.Configure<AuthorizationServiceApiSettings>(configuration.GetSection("ComponentsSettings:AuthorizationServiceApiSettings"));
            services.Configure<StreamServiceApiSettings>(configuration.GetSection("ComponentsSettings:StreamServiceApiSettings"));
            services.Configure<LicenseProxyApiSettings>(configuration.GetSection("ComponentsSettings:LicenseProxyApiSettings"));

            services.Configure<ContentMetadataServiceMockSettings>(configuration.GetSection("MockComponentsSettings:ContentMetadataServiceMockSettings"));
            services.Configure<StreamGatewayMockSettings>(configuration.GetSection("MockComponentsSettings:StreamGatewayMockSettings"));

            return services;
        }
    }
}
