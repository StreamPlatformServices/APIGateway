using APIGatewayRouting.IntegrationContracts;
using StreamGatewayMock;

namespace APIGatewayMain.ServiceCollectionExtensions
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
