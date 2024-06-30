using Microsoft.AspNetCore.Mvc;
using APIGatewayRouting.Routing.Interfaces;
using APIGatewayControllers.DataMappers;
using Microsoft.AspNetCore.Authorization;
using APIGatewayControllers.Middlewares.Attributes;
using APIGatewayControllers.Models.Requests;
using APIGatewayControllers.Models.Responses;
using APIGatewayControllers.DTO.Models.Requests;
using APIGatewayCoreUtilities.CommonExceptions;
using System.Net;
using APIGatewayRouting.Data;

namespace APIGateway.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRouter _userRouter;

        public UserController(
            ILogger<UserController> logger,
            IUserRouter userRouter)
        {
            _logger = logger;
            _userRouter = userRouter;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetUserDataAsync()
        {
            var response = new Response<UserResponseModel> { Result = new UserResponseModel() };
            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                var user = await _userRouter.GetUserAsync(jwt);
                if (user == null)
                {
                    HttpContext.Response.Headers.Append("Retry-After", "3600"); //TODO: Configurable rate limiting
                    _logger.LogWarning("Get all users. Too many requests.");
                    return StatusCode((int)HttpStatusCode.TooManyRequests, response);
                }
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
                response.Result.Token = await _userRouter.SignInAsync(requestData.Email, requestData.Password);
                
                if (string.IsNullOrEmpty(response.Result.Token))
                {
                    HttpContext.Response.Headers.Append("Retry-After", "3600"); //TODO: Configurable rate limiting
                    _logger.LogWarning("Get all users. Too many requests.");
                    return StatusCode((int)HttpStatusCode.TooManyRequests, response);
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
        public async Task<ActionResult> AddContentCreatorAsync(ContentCreatorRequestModel userModel)
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
                response.Result = await _userRouter.AddContentCreatorUserAsync(userModel.ToContentCreatorUser());
                if (!response.Result)
                {
                    HttpContext.Response.Headers.Append("Retry-After", "3600"); //TODO: Configurable rate limiting
                    _logger.LogWarning("Get all users. Too many requests.");
                    return StatusCode((int)HttpStatusCode.TooManyRequests, response);
                }

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
                _logger.LogError(ex, "An error occurred while adding a user..");
                response.Message = $"An error occurred while adding a user.. Error message: {ex.Message}";
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("end-user")]
        public async Task<ActionResult> AddEndUserAsync(EndUserRequestModel userModel)
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
                response.Result = await _userRouter.AddEndUserAsync(userModel.ToEndUser());
                if (!response.Result)
                {
                    HttpContext.Response.Headers.Append("Retry-After", "3600"); //TODO: Configurable rate limiting
                    _logger.LogWarning("Get all users. Too many requests.");
                    return StatusCode((int)HttpStatusCode.TooManyRequests, response);
                }

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
                return StatusCode(500, response);
            }
        }

        

        [Authorize(Roles = "EndUser")]
        [HttpPut("end-user")]
        public async Task<ActionResult> EditEndUserAsync(EndUserRequestModel userModel)
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
                bool result = await _userRouter.EditEndUserAsync(userModel.ToEndUser(), jwt);
                if (!result)
                {
                    HttpContext.Response.Headers.Append("Retry-After", "3600"); //TODO: Configurable rate limiting
                    _logger.LogWarning("Get all users. Too many requests.");
                    return StatusCode((int)HttpStatusCode.TooManyRequests, response);
                }

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
        public async Task<ActionResult> EditContentCreatorUserAsync(ContentCreatorRequestModel userModel)
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
                bool result = await _userRouter.EditContentCreatorUserAsync(userModel.ToContentCreatorUser(), jwt);
                if (!result)
                {
                    HttpContext.Response.Headers.Append("Retry-After", "3600"); //TODO: Configurable rate limiting
                    _logger.LogWarning("Get all users. Too many requests.");
                    return StatusCode((int)HttpStatusCode.TooManyRequests, response);
                }

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
                bool result = await _userRouter.RemoveUserAsync(requestData.Password, jwt);
                if (!result)
                {
                    HttpContext.Response.Headers.Append("Retry-After", "3600"); //TODO: Configurable rate limiting
                    _logger.LogWarning("Get all users. Too many requests.");
                    return StatusCode((int)HttpStatusCode.TooManyRequests, response);
                }

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
                var users = await _userRouter.GetAllUsersAsync(jwt);
                
                if (users == null)
                {
                    HttpContext.Response.Headers.Append("Retry-After", "3600"); //TODO: Configurable rate limiting
                    _logger.LogWarning("Get all users. Too many requests.");
                    return StatusCode((int)HttpStatusCode.TooManyRequests, response);
                }

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
                response.Result = await _userRouter.ChangeUserStatusAsync(username, requestData.Status, jwt);

                if (!response.Result)
                {
                    HttpContext.Response.Headers.Append("Retry-After", "3600"); //TODO: Configurable rate limiting
                    _logger.LogWarning("Get all users. Too many requests.");
                    return StatusCode((int)HttpStatusCode.TooManyRequests, response);
                }

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
