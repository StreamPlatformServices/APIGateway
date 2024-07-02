using APIGatewayCoreUtilities.CommonConfiguration.ConfigurationModels;
using APIGatewayRouting.IntegrationContracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationServiceAPI.ServiceCollectionExtensions
{

    public static class AuthorizationServiceAPIServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthorizationServiceAPI(this IServiceCollection services) 
        {
            services.AddHttpClient<IAuthorizationContract, AuthorizationContract>((serviceProvider, client) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<AuthorizationServiceApiSettings>>().Value;
                client.BaseAddress = new Uri(options.AuthorizationServiceUrl);
            });

            services.AddHttpClient<IUserContract, UserContract>((serviceProvider, client) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<AuthorizationServiceApiSettings>>().Value;
                client.BaseAddress = new Uri(options.AuthorizationServiceUrl);
            });

            return services;
        }
    }
    
}
