using APIGatewayRouting.Data;

namespace APIGatewayRouting.IntegrationContracts
{
    public interface IUserContract //TODO: Change name Contract to Service
    {
        //TODO: Seperate class EndUserService, ContentCreatorUserService???
        Task<User> GetUserByNameAsync(string userName, string token); 
        Task<bool> AddContentCreatorUserAsync(ContentCreatorUser user);
        Task<bool> AddEndUserAsync(EndUser user);
        Task<bool> RemoveUserAsync(Guid userId, string token);
        Task<bool> EditEndUserAsync(Guid userId, EndUser user, string token);
        Task<bool> EditContentCreatorUserAsync(Guid userId, ContentCreatorUser user, string token);
    }
}
