using APIGatewayEntities.IntegrationContracts;
using ContentMetadataServiceMock;
using ContentMetadataServiceMock.Persistance;
using Microsoft.EntityFrameworkCore;

namespace APIGatewayMain.ServiceCollectionExtensions.ComponentsExtensions
{

    public static class ContentMetadataServiceMockExtensions
    {
        public static IServiceCollection AddContentMetadataMock(this IServiceCollection services, string databasePath)
        {
            services.AddDbContext<ContentMetadataDatabaseContext>(options =>
            {
                options.UseSqlite($"Data Source={databasePath}/content.db");
            });

            services.AddScoped<IContentMetadataContract, ContentMetadataContract>();
            services.AddScoped<IContentCommentContract, ContentCommentContract>();

            return services;
        }
    }

}
