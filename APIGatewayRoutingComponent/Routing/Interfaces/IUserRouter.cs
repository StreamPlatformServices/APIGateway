using APIGatewayRouting.Data;

namespace APIGatewayRouting.Routing.Interfaces
{
    public interface IUserRouter
    {
        /// <summary>
        /// Returns null if the rate limit has been reached.
        /// </summary>
        Task<IEnumerable<User>?> GetAllUsersAsync(string token);

        /// <summary>
        /// Returns null if the rate limit has been reached.
        /// </summary>
        Task<User?> GetUserAsync(string token);

        /// <summary>
        /// Returns false if the rate limit has been reached.
        /// </summary>
        Task<bool> AddContentCreatorUserAsync(ContentCreatorUser user);

        /// <summary>
        /// Returns false if the rate limit has been reached.
        /// </summary>
        Task<bool> AddEndUserAsync(EndUser user);

        /// <summary>
        /// Returns false if the rate limit has been reached.
        /// </summary>
        Task<bool> RemoveUserAsync(string password, string token);

        /// <summary>
        /// Returns false if the rate limit has been reached.
        /// </summary>
        Task<bool> ChangePasswordAsync(string oldPassword, string newPassword, string token);

        /// <summary>
        /// Returns false if the rate limit has been reached.
        /// </summary>
        Task<bool> EditEndUserAsync(EndUser user, string token);

        /// <summary>
        /// Returns false if the rate limit has been reached.
        /// </summary>
        Task<bool> EditContentCreatorUserAsync(ContentCreatorUser user, string token);

        /// <summary>
        /// Returns null if the rate limit has been reached.
        /// </summary>
        Task<string?> SignInAsync(string login, string password);

        /// <summary>
        /// Returns false if the rate limit has been reached.
        /// </summary>
        Task<bool> ChangeUserStatusAsync(string userName, bool status, string token);
    }
}
