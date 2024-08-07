﻿using APIGatewayCoreUtilities.CommonConfiguration.ConfigurationModels;
using APIGatewayEntities.IntegrationContracts;
using AuthorizationServiceAPI;
using Microsoft.Extensions.Options;

namespace APIGatewayMain.ServiceCollectionExtensions.ComponentsExtensions
{

    public static class AuthorizationServiceAPIExtensions
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
