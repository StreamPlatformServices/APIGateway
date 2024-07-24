using APIGatewayEntities.Helpers.Interfaces;
using APIGatewayEntities.Helpers;
using APIGatewayEntities.IntegrationContracts;

namespace APIGatewayMain.ServiceCollectionExtensions.ComponentsExtensions
{
    public static class EntityExtensions
    {
        public static IServiceCollection AddEntityComponent(this IServiceCollection services)
        {
            //TODO: service provider and change this to transient
            services.AddScoped<IContentFasade, ContentFasade>();

            //TODO: Move to components extension methods??
            services.Decorate<IContentCommentContract, ContentCommentDecorator>();
            services.AddTransient<ITimeWrapper, TimeWrapper>();
            services.AddTransient<ILicenseChecker, LicenseChecker>();
            services.AddTransient<ILicenseAdapter, LicenseAdapter>();

            return services;
        }
    }

}
