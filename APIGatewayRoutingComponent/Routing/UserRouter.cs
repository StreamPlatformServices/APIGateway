using APIGatewayRouting.Data;
using APIGatewayRouting.Helpers.Interfaces;
using APIGatewayRouting.IntegrationContracts;

namespace APIGatewayRouting.Routing.Interfaces
{
    public class UserRouter : IUserRouter
    {
        //TODO: Different implementations of user type ??? (something like the repository)
        //TODO: Rate limit???
        private readonly IAuthorizationContract _authorizationContract;
        private readonly IUserContract _userContract;
        //private readonly IRateLimiter _rateLimiter;

        public UserRouter(
            IAuthorizationContract authorizationContract,
            IUserContract userContract//,
            //IRateLimiter rateLimiter
            )
        {
            _authorizationContract = authorizationContract;
            _userContract = userContract; 
            //_rateLimiter = rateLimiter;
        }

        async Task<string> IUserRouter.SignInAsync(string login, string password)
        {
            var token = await _authorizationContract.AuthorizeAsync(login, password);
            return token;
        }

        async public Task<bool> AddContentCreatorUserAsync(ContentCreatorUser user)
        {
            return await _userContract.AddContentCreatorUserAsync(user);
        }

        async public Task<bool> AddEndUserAsync(EndUser user)
        {
            return await _userContract.AddEndUserAsync(user);
        }

        async public Task<bool> EditEndUserAsync(Guid userId, EndUser user, string token)
        {
            return await _userContract.EditEndUserAsync(userId, user, token);
        }

        async public Task<bool> EditContentCreatorUserAsync(Guid userId, ContentCreatorUser user, string token)
        {
            return await _userContract.EditContentCreatorUserAsync(userId, user, token);
        }

        async Task<bool> IUserRouter.RemoveUserAsync(Guid Uuid, string token)
        {
            return await _userContract.RemoveUserAsync(Uuid, token);
        }

        async Task<User> IUserRouter.GetUserByNameAsync(string userName, string token)
        {
            return await _userContract.GetUserByNameAsync(userName, token);
        }

        
    }
}
