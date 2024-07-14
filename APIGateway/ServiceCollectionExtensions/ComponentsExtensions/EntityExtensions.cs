using APIGatewayEntities.Helpers.Interfaces;
using APIGatewayEntities.Helpers;
using APIGatewayEntities.IntegrationContracts;

namespace APIGatewayMain.ServiceCollectionExtensions.ComponentsExtensions
{
    public static class EntityExtensions
    {
        public static IServiceCollection AddEntityComponent(this IServiceCollection services)
        {
            services.AddScoped<IContentFasade, ContentFasade>();

            //TODO: Move to components extension methods??
            services.Decorate<IContentCommentContract, ContentCommentDecorator>();
            services.Decorate<ILicenseContract, LicenseDecorator>();

            return services;
        }
    }

}
