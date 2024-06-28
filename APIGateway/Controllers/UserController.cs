using Microsoft.AspNetCore.Mvc;
using APIGatewayRouting.Routing.Interfaces;
using APIGatewayControllers.DataMappers;
using Microsoft.AspNetCore.Authorization;
using APIGatewayControllers.Middlewares.Attributes;
using APIGatewayControllers.Models.Requests;
using APIGatewayControllers.Models.Responses;
using APIGatewayControllers.DTO.Models.Requests;

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
        [HttpGet()]
        public async Task<ActionResult> GetUserDataAsync()
        {
            var response = new Response<UserResponseModel> { Result = new UserResponseModel() };
            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                var user = await _userRouter.GetUserAsync(jwt);
                response.Result = user.ToUserResponseModel();

                if (response.Result == null)
                {
                    return NotFound("Provided token doesn't match to any user!");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the user data.");
                response.Message = $"An error occurred while getting the user data. Error message: {ex.Message}";
                return StatusCode(500, response);
            }
        }

        [UpdateJwtPublicKey]
        [HttpPost("sign-in")]  
        public async Task<ActionResult> SignInAsync([FromBody] SignInRequestModel requestData)
        {
            var response = new Response<SignInResponseModel> { Result = new SignInResponseModel() };
            try
            {
                response.Result.Token = await _userRouter.SignInAsync(requestData.Email, requestData.Password);
                
                if (string.IsNullOrEmpty(response.Result.Token))
                {
                    response.Message = $"Operation can not be completed at the moment. Please try again later or contact the administrator for more information.";
                    response.Result = null;
                    return StatusCode(500, response);
                }

                response.Message = $"User {requestData.Email} signed in succesfully.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while signing in.");

                response.Message = $"An error occurred while signing in. Error message: {ex.Message}";
                response.Result = null;
                return StatusCode(500, response);
            }
        }

        [HttpPost("content-creator")]
        public async Task<ActionResult> AddContentCreatorAsync(ContentCreatorRequestModel userModel) //TODO: ContentCreatorRequestModel??
        {
            //TODO: UserData validator
            //TODO: Check if user exist ??
            var response = new Response<bool> { Result = false };
            try
            {
                response.Result = await _userRouter.AddContentCreatorUserAsync(userModel.ToContentCreatorUser());
                if (!response.Result)
                {
                    response.Message = $"Operation add user can not be completed at the moment. Pleas try again later or contact the administrator for more information.";
                    return StatusCode(500, response);
                }
                response.Message = $"User {userModel.UserName} has been added successfully.";
                response.Result = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a user.");
                response.Message = $"An error occurred while adding a user. Error message: {ex.Message}";
                return StatusCode(500, response);
            }
        }

        [HttpPost("end-user")]
        public async Task<ActionResult> AddEndUserAsync(EndUserRequestModel userModel)
        {
            //TODO: UserData validator
            //TODO: Check if user exist ??
            var response = new Response<bool> { Result = false };
            try
            {
                response.Result = await _userRouter.AddEndUserAsync(userModel.ToEndUser());
                if (!response.Result)
                {
                    response.Message = $"Operation add user can not be completed at the moment. Pleas try again later or contact the administrator for more information.";
                    return StatusCode(500, response);
                }
                response.Message = $"User {userModel.UserName} has been added successfully.";
                response.Result = true;
                return Ok(response);
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
            var response = new Response<bool> { Result = false };
            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            try
            {
                bool result = await _userRouter.EditEndUserAsync(userModel.ToEndUser(), jwt);
                if (!result)
                {
                    response.Message = $"Operation edit user can not be completed at the moment. Pleas try again later or contact the administrator for more information.";
                    return StatusCode(500, response);
                }

                response.Message = $"User data has been changed successfully.";
                response.Result = true;
                return Ok(response);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while editing a user.");
                response.Message = $"An error occurred while editing a user. Error message: {ex.Message}";
                return StatusCode(500, response);
            }
        }

        [Authorize(Roles = "ContentCreator")]
        [HttpPut("content-creator")]
        public async Task<ActionResult> EditContentCreatorUserAsync(ContentCreatorRequestModel userModel)
        {
            var response = new Response<bool> { Result = false };
            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                bool result = await _userRouter.EditContentCreatorUserAsync(userModel.ToContentCreatorUser(), jwt);
                if (!result)
                {
                    response.Message = $"Operation edit user can not be completed at the moment. Pleas try again later or contact the administrator for more information.";
                    return StatusCode(500, response);
                }

                response.Message = $"User data has been changed successfully.";
                response.Result = true;
                return Ok(response);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while editing a user.");
                response.Message = $"An error occurred while editing a user. Error message: {ex.Message}";
                return StatusCode(500, response);
            }
        }

        [Authorize]
        [HttpDelete()]
        public async Task<ActionResult> RemoveUserAsync([FromBody] string password) 
        {
            var response = new Response<bool> { Result = false };
            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                bool result = await _userRouter.RemoveUserAsync(password, jwt);
                if (!result)
                {
                    response.Message = $"Operation remove user can not be completed at the moment. Pleas try again later or contact the administrator for more information.";
                    return StatusCode(500, response);
                }

                response.Message = $"User has been removed successfully.";
                response.Result = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while removing a user.");
                response.Message = $"An error occurred while removing a user. Error message: {ex.Message}";
                return StatusCode(500, response);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<ActionResult> GetAllUserAsync()
        {
            var response = new Response<UsersResponseModel>();
            //TODO: change to IEnumerable?????
            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                var users = await _userRouter.GetAllUsersAsync(jwt);
                if (users == null || !users.Any())
                {
                    response.Message = "Users list is empty!";
                    return NotFound();
                }
                response.Result = users.ToUsersResponseModel();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the user data.");
                response.Message = $"An error occurred while getting the user data. Error message: {ex.Message}";
                return StatusCode(500, response);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{username}/status")]
        public async Task<ActionResult> UpdateStatusAsync([FromRoute] string username, [FromBody] UserStatusRequestModel requestData) 
        {
            var response = new Response<bool>();
            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            try
            {
                response.Result = await _userRouter.ChangeUserStatusAsync(username, requestData.Status, jwt);

                if (!response.Result)
                {
                    response.Message = $"Operation can not be completed at the moment. Please try again later or contact the administrator for more information.";
                    return StatusCode(500, response);
                }

                response.Message = $"User status changed successfully";
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred.");

                response.Message = $"An error occurred. Error message: {ex.Message}";
                return StatusCode(500, response);
            }
        }
    }
}
