using Microsoft.AspNetCore.Mvc;
using APIGatewayRouting.Routing.Interfaces;
using APIGatewayControllers.DataMappers;
using Microsoft.AspNetCore.Authorization;
using APIGatewayRouting.Data;
using APIGatewayControllers.Middlewares.Attributes;
using APIGatewayControllers.DTO.Models;
using APIGatewayControllers.DTO.Models.Responses;

namespace APIGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [UpdateJwtPublicKey]
        [HttpPost("SignIn", Name = "SignIn")]  
        public async Task<ActionResult> SignInAsync(string email, string password) //TODO: Json Body/ change userName to email!!!!!!
        {
            var response = new Response<SignInDataResponse> { Result = new SignInDataResponse() };
            try
            {
                response.Result.Token = await _userRouter.SignInAsync(email, password);
                
                if (string.IsNullOrEmpty(response.Result.Token))
                {
                    response.Message = $"Operation can not be completed at the moment. Please try again later or contact the administrator for more information.";
                    response.Result = null;
                    return StatusCode(500, response);
                }

                response.Message = $"User {email} signed in succesfully.";
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

        [HttpPost("AddContentCreator", Name = "AddContentCreator")]
        public async Task<ActionResult> AddContentCreatorAsync(ContentCreatorUserModel userModel)
        {
            //TODO: UserData validator
            //TODO: Check if user exist
            var response = new Response<bool> { Result = false };
            try
            {
                response.Result = await _userRouter.AddContentCreatorUserAsync(userModel.ToContentCreatorUser());
                if (!response.Result)
                {
                    response.Message = $"Operation add user can not be completed at the moment. Pleas try again later or contact the administrator for more information.";
                    return StatusCode(500, response);
                }
                response.Message = $"User {userModel.Name} has been added successfully.";
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

        [HttpPost("AddEndUser", Name = "AddEndUser")]
        public async Task<ActionResult> AddEndUserAsync(EndUserModel userModel)
        {
            //TODO: UserData validator
            //TODO: Check if user exist
            var response = new Response<bool> { Result = false };
            try
            {
                response.Result = await _userRouter.AddEndUserAsync(userModel.ToEndUser());
                if (!response.Result)
                {
                    response.Message = $"Operation add user can not be completed at the moment. Pleas try again later or contact the administrator for more information.";
                    return StatusCode(500, response);
                }
                response.Message = $"User {userModel.Name} has been added successfully.";
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

        [Authorize]
        [HttpGet("Get", Name = "GetUserData")]
        public async Task<ActionResult> GetUserDataAsync(string userName)
        {
            var response = new Response<UserModel> { Result = new UserModel() };
            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                var user = await _userRouter.GetUserByNameAsync(userName, jwt);

                if (user is EndUser endUser)
                {
                    response.Result = endUser.ToEndUserModel();
                    return Ok(response);
                }

                if (user is ContentCreatorUser contentCreatorUser)
                {
                    response.Result = contentCreatorUser.ToContentCreatorUserModel();
                    return Ok(response);
                }

                response.Message = $"User with name: {userName} doesn't exist!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the user data.");
                response.Message = $"An error occurred while getting the user data. Error message: {ex.Message}";
                return StatusCode(500, response);
            }
        }

        //TODO: Get all users for admin

        [Authorize]
        [HttpDelete("Delete", Name = "RemoveUser")]
        public async Task<ActionResult> RemoveUserAsync(string userName) //TODO: Rozkmin jeszcze czy wyszukujemy po userName (moze + data) 
        {
            //TODO: User == null ???

            var response = new Response<bool> { Result = false };
            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                var user = await _userRouter.GetUserByNameAsync(userName, jwt);
                if (user == null)
                {
                    response.Message = $"User with name: {userName} doesn't exist!";
                    return NotFound();
                }

                bool result = await _userRouter.RemoveUserAsync(user.Uuid, jwt);
                if (!result)
                {
                    response.Message = $"Operation remove user can not be completed at the moment. Pleas try again later or contact the administrator for more information.";
                    return StatusCode(500, response);
                }

                response.Message = $"User {userName} has been removed successfully.";
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

        [Authorize(Roles = "EndUser")]
        [HttpPut("EditEndUser", Name = "EditEndUser")]
        public async Task<ActionResult> EditEndUserAsync(string userName, EndUserModel userModel)
        {
            var response = new Response<bool> { Result = false };
            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            try
            {
                var user = await _userRouter.GetUserByNameAsync(userName, jwt);
                if (user is null or not EndUser)
                {
                    response.Message = $"User with name: {userName} doesn't exist!";
                    return NotFound(response);
                }

                bool result = await _userRouter.EditEndUserAsync(user.Uuid, userModel.ToEndUser(), jwt);
                if (!result)
                {
                    response.Message = $"Operation edit user can not be completed at the moment. Pleas try again later or contact the administrator for more information.";
                    return StatusCode(500, response);
                }

                response.Message = $"User data for {userName} has been changed successfully.";
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
        [HttpPut("EditContentCreatorUser", Name = "EditContentCreatorUser")]
        public async Task<ActionResult> EditContentCreatorUserAsync(string userName, ContentCreatorUserModel userModel)
        {
            var response = new Response<bool> { Result = false };
            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                var user = await _userRouter.GetUserByNameAsync(userName, jwt);
                if (user is null or not ContentCreatorUser)
                {
                    response.Message = $"User with name: {userName} doesn't exist!";
                    return NotFound(response);
                }

                bool result = await _userRouter.EditContentCreatorUserAsync(user.Uuid, userModel.ToContentCreatorUser(), jwt);
                if (!result)
                {
                    response.Message = $"Operation edit user can not be completed at the moment. Pleas try again later or contact the administrator for more information.";
                    return StatusCode(500, response);
                }

                response.Message = $"User data for {userName} has been changed successfully.";
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
    }
}
