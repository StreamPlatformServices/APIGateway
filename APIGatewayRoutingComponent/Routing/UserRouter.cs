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

        async Task<bool> IUserRouter.AddContentCreatorUserAsync(ContentCreatorUser user)
        {
            return await _userContract.AddContentCreatorUserAsync(user);
        }

        async Task<bool> IUserRouter.AddEndUserAsync(EndUser user)
        {
            return await _userContract.AddEndUserAsync(user);
        }

        async Task<bool> IUserRouter.EditEndUserAsync(EndUser user, string token)
        {
            return await _userContract.EditEndUserAsync(user, token);
        }

        async Task<bool> IUserRouter.EditContentCreatorUserAsync(ContentCreatorUser user, string token)
        {
            return await _userContract.EditContentCreatorUserAsync(user, token);
        }

        async Task<bool> IUserRouter.RemoveUserAsync(string token)
        {
            return await _userContract.RemoveUserAsync(token);
        }

        async Task<User> IUserRouter.GetUserAsync(string token)
        {
            return await _userContract.GetUserAsync(token);
        }

        async Task<IEnumerable<User>> IUserRouter.GetAllUsersAsync(string token)
        {
            return await _userContract.GetAllUsersAsync(token);
        }

        async Task<bool> IUserRouter.ChangeUserStatusAsync(string userName, bool status, string token)
        {
            return await _userContract.ChangeUserStatusAsync(userName, status, token);
        }
    }
}
