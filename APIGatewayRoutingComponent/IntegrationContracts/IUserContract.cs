using APIGatewayRouting.Data;

namespace APIGatewayRouting.IntegrationContracts
{
    public interface IUserContract
    {
        //TODO: Seperate class EndUserService, ContentCreatorUserService???
        Task<User> GetUserAsync(string token); 
        Task<IEnumerable<User>> GetAllUsersAsync(string token); 
        Task AddContentCreatorUserAsync(ContentCreatorUser user);
        Task AddEndUserAsync(EndUser user);
        Task RemoveUserAsync(string password, string token);
        Task EditEndUserAsync(EndUser user, string token);
        Task EditContentCreatorUserAsync(ContentCreatorUser user, string token);
        Task ChangeUserStatusAsync(string userName, bool status, string token);
    }
}
