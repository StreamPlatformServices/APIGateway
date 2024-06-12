using APIGatewayRouting.IntegrationContracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationServiceAPI.ServiceCollectionExtensions
{

    public static class AuthorizationServiceAPIServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthorizationServiceAPI(this IServiceCollection services, string authorizationServiceUrl) 
            //TODO: Zastanow sie nad tym (czy moze nie przeniesc tej metody do ControllersComponent)
            //Skoro i tak cala konfiguracja jest ustawiana w controllers component 
            //TODO: Mi przy projektowaniu chodzilo o to aby ustawienia podlaczenia do mikroserwisu byly zalezne od dll'ki, wiec chyba powinien byc zahardkodowany normalnie w klasie url
            //TODO: Jeszcze przegadaj to z chatem... hmmm bo wydaje sie, ze ta koncepcja moze byc dosc niewygodna. API Gateway powinien miec jedna konfiguracje ktora ustawia wszystkie komunikacje ze wszystkimi serwisami
            //dll'ka to tylko logika ..  zastanow sie czy przenosic informacje o mockach do konfiguracji... moze poprostu puste bedzie oznaczalo, ze to mock 
        {
            services.AddHttpClient<IAuthorizationContract, AuthorizationContract>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7124");
            });

            services.AddHttpClient<IUserContract, UserContract>(client =>
            {
                client.BaseAddress = new Uri(authorizationServiceUrl);
            });

            return services;
        }
    }
    
}
