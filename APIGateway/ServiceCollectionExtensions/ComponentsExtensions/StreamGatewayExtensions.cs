using APIGatewayCoreUtilities.CommonConfiguration.ConfigurationModels;
using APIGatewayEntities.IntegrationContracts;
using AuthorizationServiceAPI;
using Microsoft.Extensions.Options;
using StreamGatewayAPI;

namespace APIGatewayMain.ServiceCollectionExtensions.ComponentsExtensions
{

    public static class StreamGatewayExtensions
    {
        public static IServiceCollection AddStreamGatewayAPI(this IServiceCollection services)
        {
            services.AddHttpClient<IStreamUriContract, StreamUriContract>((serviceProvider, client) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<StreamGatewayApiSettings>>().Value;
                client.BaseAddress = new Uri(options.StreamGatewayUrl);
            });

            return services;
        }

        //public static IServiceCollection AddStreamGatewayMock(this IServiceCollection services)
        //{
        //    services.AddTransient<IStreamUriContract, StreamGatewayContract>();

        //    return services;
        //}
    }

}
