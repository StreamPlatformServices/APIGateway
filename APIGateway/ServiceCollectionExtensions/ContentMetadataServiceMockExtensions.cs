using APIGatewayCoreUtilities.CommonConfiguration.ConfigurationModels.MockSettings;
using APIGatewayRouting.IntegrationContracts;
using ContentMetadataServiceMock;
using ContentMetadataServiceMock.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace APIGatewayMain.ServiceCollectionExtensions
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
            return services;
        }
    }
    
}
