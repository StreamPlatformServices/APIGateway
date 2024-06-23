using APIGatewayRouting.IntegrationContracts;
using ContentMetadataServiceMock.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace ContentMetadataServiceMock.ServiceCollectionExtensions
{

    public static class ContentMetadataServiceMockCollectionExtensions
    {
        public static IServiceCollection AddContentMetadataMock(this IServiceCollection services, string databasePath) 
        {
            services.AddDbContext<ContentMetadataDatabaseContext>(options =>
            options.UseSqlite("Data Source=../ContentMetadataServiceMock/content.db"));

            services.AddScoped<IContentMetadataContract, ContentMetadataContract>();
            return services;
        }
    }
    
}
