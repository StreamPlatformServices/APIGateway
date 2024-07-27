using APIGatewayCoreUtilities.CommonExceptions;
using APIGatewayEntities.Entities;
using System.Text.Json;

namespace APIGatewayEntities.IntegrationContracts
{
    //TODO: Seperate class EndUserService, ContentCreatorUserService???
    public interface IUserContract
    {
        /// <exception cref="NotFoundException">Thrown when the user is not found.</exception>
        /// <exception cref="UnauthorizedException">Thrown when the token is unauthorized.</exception>
        /// <exception cref="ForbiddenException">Thrown when access is forbidden.</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs.</exception>
        Task<User> GetUserAsync(string token);

        /// <exception cref="UnauthorizedException">Thrown when the token is unauthorized.</exception>
        /// <exception cref="ForbiddenException">Thrown when access is forbidden.</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs.</exception>
        Task<IEnumerable<User>> GetAllUsersAsync(string token);

        /// <exception cref="ConflictException">Thrown when there is a conflict with the current state of the resource.</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs.</exception>
        Task AddContentCreatorUserAsync(ContentCreatorUser user);
        
        /// <exception cref="ConflictException">Thrown when there is a conflict with the current state of the resource.</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs.</exception>
        Task AddEndUserAsync(EndUser user);

        /// <exception cref="NotFoundException">Thrown when the user is not found.</exception>
        /// <exception cref="UnauthorizedException">Thrown when the token is unauthorized.</exception>
        /// <exception cref="ForbiddenException">Thrown when access is forbidden.</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs.</exception>
        Task RemoveUserAsync(string password, string token);
        
        /// <exception cref="NotFoundException">Thrown when the user is not found.</exception>
        /// <exception cref="UnauthorizedException">Thrown when the token is unauthorized.</exception>
        /// <exception cref="ForbiddenException">Thrown when access is forbidden.</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs.</exception>
        Task EditEndUserAsync(EndUser user, string token);

        /// <exception cref="NotFoundException">Thrown when the user is not found.</exception>
        /// <exception cref="UnauthorizedException">Thrown when the token is unauthorized.</exception>
        /// <exception cref="ForbiddenException">Thrown when access is forbidden.</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs.</exception>
        Task EditContentCreatorUserAsync(ContentCreatorUser user, string token);

        /// <exception cref="NotFoundException">Thrown when the user is not found.</exception>
        /// <exception cref="UnauthorizedException">Thrown when the token is unauthorized.</exception>
        /// <exception cref="ForbiddenException">Thrown when access is forbidden.</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs.</exception>
        Task ChangeUserStatusAsync(string userName, bool status, string token);

        /// <exception cref="NotFoundException">Thrown when the user is not found.</exception>
        /// <exception cref="UnauthorizedException">Thrown when the token is unauthorized.</exception>
        /// <exception cref="ForbiddenException">Thrown when access is forbidden.</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs.</exception>
        Task ChangePasswordAsync(string oldPassword, string newPassword, string token);
    }
}
