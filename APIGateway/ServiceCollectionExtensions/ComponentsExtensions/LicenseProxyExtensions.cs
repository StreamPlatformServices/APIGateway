using APIGatewayEntities.IntegrationContracts;
using APIGatewayMicroservicesClient;

namespace APIGatewayMain.ServiceCollectionExtensions.ComponentsExtensions
{
    public static class LicenseProxyExtensions
    {
        public static IServiceCollection AddLicenseProxyApi(this IServiceCollection services)
        {
            services.AddTransient<ILicenseContract, LicenseProxyAPI>();

            return services;
        }
    }

}
