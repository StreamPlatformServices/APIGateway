using APIGatewayRouting.Data;

namespace APIGatewayRouting.Routing.Interfaces
{
    public interface IUserRouter
    {
        Task<User> GetUserByNameAsync(string userName, string token);
        Task<bool> AddContentCreatorUserAsync(ContentCreatorUser user);
        Task<bool> AddEndUserAsync(EndUser user);
        Task<bool> RemoveUserAsync(Guid userId, string token);
        Task<bool> EditEndUserAsync(Guid userId, EndUser user, string token);
        Task<bool> EditContentCreatorUserAsync(Guid userId, ContentCreatorUser user, string token);
        Task<string> SignInAsync(string login, string password);
    }
}
