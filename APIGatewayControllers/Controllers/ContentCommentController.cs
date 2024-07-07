using Microsoft.AspNetCore.Mvc;
using APIGatewayControllers.DataMappers;
using Microsoft.Extensions.Logging;
using APIGatewayEntities.IntegrationContracts;
using APIGatewayControllers.Models.Responses;
using APIGatewayCoreUtilities.CommonExceptions;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using APIGatewayControllers.Models.Requests.Comment;

namespace APIGateway.Controllers
{
    [ApiController]
    [Route("comment")]
    public class ContentCommentController : ControllerBase
    {
        private readonly ILogger<ContentCommentController> _logger;
        private readonly IContentCommentContract _contentCommentContract;

        public ContentCommentController(
            ILogger<ContentCommentController> logger,
            IContentCommentContract contentCommentContract)
        {
            _logger = logger;
            _contentCommentContract = contentCommentContract;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<string>> AddCommentAsync(ContentCommentRequestModel contentCommentModel)
        {
            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

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
                await _contentCommentContract.AddCommentAsync(contentCommentModel.ToContentComment(), jwt);
                //if (!response.Result)
                //{
                //    HttpContext.Response.Headers.Append("Retry-After", "3600"); //TODO: Configurable rate limiting
                //    _logger.LogWarning("Add content creator. Too many requests.");
                //    return StatusCode((int)HttpStatusCode.TooManyRequests, response);
                //}

                _logger.LogInformation($"Comment has been added successfully.");
                response.Message = $"Comment has been added successfully.";
                response.Result = true;
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.Message = ex.Message;
                return NotFound(response);
            }
            //catch (ConflictException ex)
            //{
            //    response.Message = ex.Message;
            //    return Conflict(response);
            //}
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
                _logger.LogError(ex, "An error occurred while adding a comment.");
                response.Message = $"An error occurred while adding a comment. Error message: {ex.Message}";
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

    }
}
