﻿using APIGatewayCoreUtilities.CommonConfiguration.ConfigurationModels.MockSettings;
using APIGatewayCoreUtilities.CommonConfiguration.ConfigurationModels;
using AspNetCoreRateLimit;
using Nethereum.JsonRpc.Client;

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
            services.Configure<LicenseProxyApiSettings>(configuration.GetSection("ComponentsSettings:LicenseProxyApiSettings"));

            services.Configure<ContentMetadataServiceMockSettings>(configuration.GetSection("MockComponentsSettings:ContentMetadataServiceMockSettings"));
            services.Configure<StreamGatewayMockSettings>(configuration.GetSection("MockComponentsSettings:StreamGatewayMockSettings"));

            return services;
        }
    }
}