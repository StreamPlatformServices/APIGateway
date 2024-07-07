using APIGatewayEntities.Helpers.Interfaces;
using APIGatewayEntities.Helpers;
using APIGatewayEntities.IntegrationContracts;
using ContentMetadataServiceMock;

namespace APIGatewayMain.ServiceCollectionExtensions.ComponentsExtensions
{
    public static class EntityExtensions
    {
        public static IServiceCollection AddEntityComponent(this IServiceCollection services)
        {
            services.AddScoped<IContentFasade, ContentFasade>();

            services.Decorate<IContentCommentContract, ContentCommentDecorator>();

            return services;
        }
    }

}
