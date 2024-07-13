using Microsoft.AspNetCore.Mvc;
using APIGatewayControllers.DataMappers;
using Microsoft.AspNetCore.Authorization;
using APIGatewayControllers.Middlewares.Attributes;
using APIGatewayCoreUtilities.CommonExceptions;
using System.Net;
using APIGatewayEntities.Entities;
using Microsoft.Extensions.Logging;
using APIGatewayEntities.IntegrationContracts;
using APIGatewayControllers.Models.Requests.User;
using APIGatewayControllers.Models.Responses.User;
using APIGatewayControllers.Models.Responses;

namespace APIGateway.Controllers
{
    //TODO: Log start operations of endpoints!!!!!!!!!!!!!!!!!!!!
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserContract _userContract;
        private readonly IAuthorizationContract _authorizationContract;

        public UserController(
            ILogger<UserController> logger,
            IUserContract userContract,
            IAuthorizationContract authorizationContract)
        {
            _logger = logger;
            _userContract = userContract;
            _authorizationContract = authorizationContract;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetUserDataAsync()
        {
            var response = new Response<UserResponseModel> { Result = new UserResponseModel() };
            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                var user = await _userContract.GetUserAsync(jwt);
                
                response.Result = user.ToUserResponseModel();

                if (response.Result.UserLevel == UserLevel.Unknown)
                {
                    _logger.LogError("Get user data error: Wrong user type!");
                    return NotFound("Provided token doesn't match to any user type!");
                }

                _logger.LogInformation($"Get data for user: {response.Result.UserName} succeed.");
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.Message = ex.Message;
                return NotFound(response);
            }
            catch (UnauthorizedException ex)
            {
                response.Message = ex.Message;
                return Unauthorized(response);
            }
            catch (ForbiddenException ex)
            {
                response.Message = ex.Message;
                return StatusCode((int)HttpStatusCode.Forbidden, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the user data.");
                response.Message = $"An error occurred while getting the user data. Error message: {ex.Message}";
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [UpdateJwtPublicKey]
        [HttpPost("sign-in")]  
        public async Task<ActionResult> SignInAsync([FromBody] SignInRequestModel requestData)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage);

                _logger.LogWarning("Invalid model state for AddEndUserAsync: {Errors}", string.Join("; ", errorMessages));
                return BadRequest(ModelState);
            }

            var response = new Response<SignInResponseModel> { Result = new SignInResponseModel() };
            try
            {
                response.Result.Token = await _authorizationContract.AuthorizeAsync(requestData.Email, requestData.Password);
                
                if (string.IsNullOrEmpty(response.Result.Token))
                {
                    throw new Exception("Token is empty!");
                }

                _logger.LogInformation($"Get data for user: {requestData.Email} signed in successfully."); //TODO: email should be logged??
                response.Message = $"User {requestData.Email} signed in succesfully.";
                return Ok(response);
            }
            catch (UnauthorizedException ex)
            {
                response.Message = ex.Message;
                return Unauthorized(response);
            }
            catch (ForbiddenException ex)
            {
                response.Message = ex.Message;
                return StatusCode((int)HttpStatusCode.Forbidden, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while signing in.");
                response.Message = $"An error occurred while signing in. Error message: {ex.Message}";
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("content-creator")]
        public async Task<ActionResult> AddContentCreatorAsync(AddContentCreatorRequestModel userModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage);

                _logger.LogWarning("Invalid model state for AddEndUserAsync: {Errors}", string.Join("; ", errorMessages));
                return BadRequest(ModelState);
            }

            var response = new Response<bool> { Result = false };
            try
            {
                await _userContract.AddContentCreatorUserAsync(userModel.ToContentCreatorUser());

                _logger.LogInformation($"User {userModel.UserName} has been added successfully.");
                response.Message = $"User {userModel.UserName} has been added successfully.";
                response.Result = true;
                return Ok(response);
            }
            catch (ConflictException ex)
            {
                response.Message = ex.Message;
                return Conflict(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a user.");
                response.Message = $"An error occurred while adding a user. Error message: {ex.Message}";
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("end-user")]
        public async Task<ActionResult> AddEndUserAsync(AddEndUserRequestModel userModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage);

                _logger.LogWarning("Invalid model state for AddEndUserAsync: {Errors}", string.Join("; ", errorMessages));
                return BadRequest(ModelState);
            }

            var response = new Response<bool> { Result = false };
            try
            {
                await _userContract.AddEndUserAsync(userModel.ToEndUser());

                _logger.LogInformation($"User {userModel.UserName} has been added successfully.");
                response.Message = $"User {userModel.UserName} has been added successfully.";
                response.Result = true;
                return Ok(response);
            }
            catch (ConflictException ex)
            {
                response.Message = ex.Message;
                return Conflict(response);
            }
            catch (Exception ex)
            {   
                _logger.LogError(ex, "An error occurred while adding a user.");
                response.Message = $"An error occurred while adding a user. Error message: {ex.Message}";
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        

        [Authorize(Roles = "EndUser")]
        [HttpPut("end-user")]
        public async Task<ActionResult> EditEndUserAsync(UpdateEndUserRequestModel userModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage);

                _logger.LogWarning("Invalid model state for AddEndUserAsync: {Errors}", string.Join("; ", errorMessages));
                return BadRequest(ModelState);
            }

            var response = new Response<bool> { Result = false };
            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                await _userContract.EditEndUserAsync(userModel.ToEndUser(), jwt);

                _logger.LogInformation("User data has been changed successfully.");
                response.Message = "User data has been changed successfully.";
                response.Result = true;
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.Message = ex.Message;
                return NotFound(response);
            }
            catch (UnauthorizedException ex)
            {
                response.Message = ex.Message;
                return Unauthorized(response);
            }
            catch (ForbiddenException ex)
            {
                response.Message = ex.Message;
                return StatusCode((int)HttpStatusCode.Forbidden, response);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while editing a user."); //TODO: test ex
                response.Message = $"An error occurred while editing a user. Error message: {ex.Message}";
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [Authorize(Roles = "ContentCreator")]
        [HttpPut("content-creator")]
        public async Task<ActionResult> EditContentCreatorUserAsync(UpdateContentCreatorRequestModel userModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage);

                _logger.LogWarning("Invalid model state for AddEndUserAsync: {Errors}", string.Join("; ", errorMessages));
                return BadRequest(ModelState);
            }

            var response = new Response<bool> { Result = false };
            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                await _userContract.EditContentCreatorUserAsync(userModel.ToContentCreatorUser(), jwt);

                _logger.LogInformation("User data has been changed successfully.");
                response.Message = $"User data has been changed successfully.";
                response.Result = true;
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.Message = ex.Message;
                return NotFound(response);
            }
            catch (UnauthorizedException ex)
            {
                response.Message = ex.Message;
                return Unauthorized(response);
            }
            catch (ForbiddenException ex)
            {
                response.Message = ex.Message;
                return StatusCode((int)HttpStatusCode.Forbidden, response);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while editing a user.");
                response.Message = $"An error occurred while editing a user. Error message: {ex.Message}";
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [Authorize]
        [HttpPatch("password")]
        public async Task<ActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequestModel requestData)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage);

                _logger.LogWarning("Invalid model state for AddEndUserAsync: {Errors}", string.Join("; ", errorMessages));
                return BadRequest(ModelState);
            }

            var response = new Response<bool> { Result = false };
            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                await _userContract.ChangePasswordAsync(requestData.OldPassword, requestData.NewPassword, jwt);

                _logger.LogInformation("User has been removed successfully.");
                response.Message = $"User has been removed successfully.";
                response.Result = true;
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.Message = ex.Message;
                return NotFound(response);
            }
            catch (UnauthorizedException ex)
            {
                response.Message = ex.Message;
                return Unauthorized(response);
            }
            catch (ForbiddenException ex)
            {
                response.Message = ex.Message;
                return StatusCode((int)HttpStatusCode.Forbidden, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while removing a user.");
                response.Message = $"An error occurred while removing a user. Error message: {ex.Message}";
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> RemoveUserAsync([FromBody] RemoveUserRequestModel requestData) 
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage);

                _logger.LogWarning("Invalid model state for AddEndUserAsync: {Errors}", string.Join("; ", errorMessages));
                return BadRequest(ModelState);
            }

            var response = new Response<bool> { Result = false };
            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                await _userContract.RemoveUserAsync(requestData.Password, jwt);

                _logger.LogInformation("User has been removed successfully.");
                response.Message = $"User has been removed successfully.";
                response.Result = true;
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.Message = ex.Message;
                return NotFound(response);
            }
            catch (UnauthorizedException ex)
            {
                response.Message = ex.Message;
                return Unauthorized(response);
            }
            catch (ForbiddenException ex)
            {
                response.Message = ex.Message;
                return StatusCode((int)HttpStatusCode.Forbidden, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while removing a user.");
                response.Message = $"An error occurred while removing a user. Error message: {ex.Message}";
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<ActionResult> GetAllUserAsync()
        {
            var response = new Response<UsersResponseModel>();

            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                var users = await _userContract.GetAllUsersAsync(jwt);

                if (!users.Any())
                {
                    return NoContent();
                }

                response.Result = users.ToUsersResponseModel();

                _logger.LogInformation("Get all users finished properly.");
                return Ok(response);
            }
            catch (UnauthorizedException ex)
            {
                response.Message = ex.Message;
                return Unauthorized(response);
            }
            catch (ForbiddenException ex)
            {
                response.Message = ex.Message;
                return StatusCode((int)HttpStatusCode.Forbidden, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the user data.");
                response.Message = $"An error occurred while getting the user data. Error message: {ex.Message}";
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{username}/status")]
        public async Task<ActionResult> UpdateStatusAsync([FromRoute] string username, [FromBody] UserStatusRequestModel requestData) 
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage);

                _logger.LogWarning("Invalid model state for AddEndUserAsync: {Errors}", string.Join("; ", errorMessages));
                return BadRequest(ModelState);
            }

            var response = new Response<bool>();
            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            try
            {
                await _userContract.ChangeUserStatusAsync(username, requestData.Status, jwt);

                _logger.LogInformation("User status changed successfully");
                response.Message = "User status changed successfully";
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.Message = ex.Message;
                return NotFound(response);
            }
            catch (UnauthorizedException ex)
            {
                response.Message = ex.Message;
                return Unauthorized(response);
            }
            catch (ForbiddenException ex)
            {
                response.Message = ex.Message;
                return StatusCode((int)HttpStatusCode.Forbidden, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while user status update.");
                response.Message = $"An error occurred. Error message: {ex.Message}";
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
    }
}
