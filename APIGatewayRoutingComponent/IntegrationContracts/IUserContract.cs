using APIGatewayRouting.Data;

namespace APIGatewayRouting.IntegrationContracts
{
    public interface IUserContract //TODO: Change name Contract to Service??
    {
        //TODO: Seperate class EndUserService, ContentCreatorUserService???
        Task<User> GetUserAsync(string token); 
        Task<IEnumerable<User>> GetAllUsersAsync(string token); 
        Task<bool> AddContentCreatorUserAsync(ContentCreatorUser user);
        Task<bool> AddEndUserAsync(EndUser user);
        Task<bool> RemoveUserAsync(string token);
        Task<bool> EditEndUserAsync(EndUser user, string token);
        Task<bool> EditContentCreatorUserAsync(ContentCreatorUser user, string token);
        Task<bool> ChangeUserStatusAsync(string userName, bool status, string token);
    }
}
