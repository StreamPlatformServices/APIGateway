using APIGatewayEntities.IntegrationContracts;
using StreamGatewayMock;

namespace APIGatewayMain.ServiceCollectionExtensions.ComponentsExtensions
{

    public static class StreamGatewayExtensions
    {
        public static IServiceCollection AddStreamGatewayMock(this IServiceCollection services)
        {
            services.AddTransient<IStreamUriContract, StreamGatewayContract>();

            return services;
        }
    }

}
