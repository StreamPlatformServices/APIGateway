using APIGatewayRouting.Data;

namespace APIGatewayRouting.Helpers.Interfaces
{
    public interface IAuthorizer
    {
        Task<bool> AuthorizeAsync(string login, string password, string sessionToken);
    }
}
