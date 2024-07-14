using APIGatewayEntities.Entities;
using APIGatewayEntities.IntegrationContracts;
using LicenseProxyAPI;
using LicenseProxyAPI.Helpers;

namespace APIGatewayMain.ServiceCollectionExtensions.ComponentsExtensions
{
    public static class LicenseProxyExtensions
    {
        public static IServiceCollection AddLicenseProxyApi(this IServiceCollection services)
        {
            services.AddTransient<ILicenseDurationCalculator, LicenseDurationCalculator>();
            services.AddTransient<ILicenseContract, LicenseContract>();

            return services;
        }
    }

}
