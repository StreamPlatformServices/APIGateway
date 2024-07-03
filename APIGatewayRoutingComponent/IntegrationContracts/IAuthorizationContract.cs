
namespace APIGatewayRouting.IntegrationContracts
{
    public interface IAuthorizationContract
    {
       Task<string> AuthorizeAsync(string login, string password);
       Task<string> GetTokenPublicKey();
    }
}
