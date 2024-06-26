using APIGatewayRouting.Data;

namespace APIGatewayRouting.Routing.Interfaces
{
    public interface IUserRouter
    {
        Task<IEnumerable<User>> GetAllUsersAsync(string token);
        Task<User> GetUserAsync(string token);
        Task<bool> AddContentCreatorUserAsync(ContentCreatorUser user);
        Task<bool> AddEndUserAsync(EndUser user);
        Task<bool> RemoveUserAsync(string token);
        Task<bool> EditEndUserAsync(EndUser user, string token);
        Task<bool> EditContentCreatorUserAsync(ContentCreatorUser user, string token);
        Task<string> SignInAsync(string login, string password);
        Task<bool> ChangeUserStatusAsync(string userName, bool status, string token);
    }
}
