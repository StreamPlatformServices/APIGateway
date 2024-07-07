using APIGatewayCoreUtilities.CommonConfiguration.ConfigurationModels.MockSettings;
using APIGatewayEntities.Helpers;
using APIGatewayEntities.IntegrationContracts;
using ContentMetadataServiceMock;
using ContentMetadataServiceMock.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace APIGatewayMain.ServiceCollectionExtensions.ComponentsExtensions
{

    public static class ContentMetadataServiceMockExtensions
    {
        public static IServiceCollection AddContentMetadataMock(this IServiceCollection services)
        {
            services.AddDbContext<ContentMetadataDatabaseContext>((serviceProvider, options) =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<ContentMetadataServiceMockSettings>>().Value;
                options.UseSqlite($"Data Source={settings.DatabasePath}");
            });

            services.AddScoped<IContentMetadataContract, ContentMetadataContract>();
            services.AddScoped<IContentCommentContract, ContentCommentContract>();

            return services;
        }
    }

}
