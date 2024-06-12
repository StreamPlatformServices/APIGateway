using APIGatewayRouting.Data;
using APIGatewayRouting.Helpers.Interfaces;
using APIGatewayRouting.IntegrationContracts;

namespace APIGatewayRouting.Helpers
{
    internal class Authorizer : IAuthorizer
    {
        private readonly IAuthorizationContract _authorizationContract;
        private readonly ISessionTokenCacher _tokenCacher;

        public Authorizer(
            IAuthorizationContract authorizationContract,
            ISessionTokenCacher tokenCacher)
        {
            _authorizationContract = authorizationContract;
            _tokenCacher = tokenCacher;
        }

        async Task<bool> IAuthorizer.AuthorizeAsync(string login, string password, string sessionToken)
        {
            if (_tokenCacher.CheckToken(login, sessionToken)) //TODO: Async?? It is not a waiting operation... CHeck if it makes sense with chat
            {
                return true;
            }

            //if (await _authorizationContract.AuthorizeAsync(login, password, sessionToken))
            //{
            //    _tokenCacher.CacheToken(login, sessionToken);
            //    return true;
            //}

            return false;
        }
    }
}
