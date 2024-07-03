using APIGatewayRouting.Routing.Interfaces;
using APIGatewayRouting.Routing;

namespace APIGatewayMain.ServiceCollectionExtensions
{
    public static class RoutingExtensions
    {
        public static IServiceCollection AddRoutingComponent(this IServiceCollection services) 
        {
            services.AddTransient<IContentRouter, ContentRouter>();
            services.AddTransient<IUserRouter, UserRouter>();
            services.AddTransient<IContentCommentRouter, ContentCommentRouter>();
            services.AddTransient<IStreamUriRouter, StreamUriRouter>();

            return services;
        }
    }
    
}
