using APIGatewayCoreUtilities.CommonConfiguration.ConfigurationModels;
using APIGatewayEntities.IntegrationContracts;
using LicenseProxyAPI;
using LicenseProxyAPI.Helpers;
using Microsoft.Extensions.Options;

namespace APIGatewayMain.ServiceCollectionExtensions.ComponentsExtensions
{
    public static class LicenseServiceClientExtensions
    {
        public static IServiceCollection AddLicenseServiceClient(this IServiceCollection services)
        {
            services.AddTransient<ILicenseTimeCalculator, LicenseTimeCalculator>();
            services.AddHttpClient<ILicenseContract, LicenseContract>((serviceProvider, client) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<LicenseServiceClientSettings>>().Value;
                client.BaseAddress = new Uri(options.LicenseServiceUrl);
            });

            return services;
        }
    }

}
