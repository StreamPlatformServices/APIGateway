using Microsoft.AspNetCore.Mvc;
using APIGatewayControllers.DataMappers;
using Microsoft.Extensions.Logging;
using APIGatewayEntities.Helpers.Interfaces;
using APIGatewayControllers.Models.Responses;
using APIGatewayCoreUtilities.CommonExceptions;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using APIGatewayControllers.Models.Responses.Content;
using APIGatewayControllers.Models.Requests.Content;

namespace APIGateway.Controllers
{

    //TODO: Log start operations of endpoints!!!!!!!!!!!!!!!!!!!!
    [ApiController]
    [Route("content")]
    public class ContentController : ControllerBase
    {
        private readonly ILogger<ContentController> _logger;
        private readonly IContentFasade _contentFasade;

        public ContentController(
            ILogger<ContentController> logger,
            IContentFasade contentFasade)
        {
            _logger = logger;
            _contentFasade = contentFasade;
        }

        //TOOD: Remove handling of unauthorized exceptions (where there is no communication with authorization service)...

        [HttpGet("all")]
        public async Task<ActionResult> GetContentsAsync([FromQuery] int limit, [FromQuery] int offset)
        {
            //TODO: Content cacher in fasade???
            var response = new Response<GetAllContentsResponseModel>();

            try
            {
                var content = await _contentFasade.GetAllContentsAsync(limit, offset);
                if (!content.Any())
                {
                    return NoContent();
                }


                _logger.LogInformation("Get all contents finished properly.");
                response.Result = content.ToGetAllContentsResponseModel();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting contents list.");
                response.Message = $"An error occurred while getting contents list. Error message: {ex.Message}";
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetContentByIdAsync(Guid uuid)
        {
            var response = new Response<GetContentResponseModel>();
            try
            {
                var content = await _contentFasade.GetContentByIdAsync(uuid);

                _logger.LogInformation("Get content data finished properly.");
                response.Result = content.ToGetContentResponseModel();
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.Message = ex.Message;
                return NotFound(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the content data.");
                response.Message = $"An error occurred while getting the content data. Error message: {ex.Message}";
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [Authorize(Roles = "ContentCreator")]
        [HttpGet("user")]
        public async Task<ActionResult> GetContentsByUser()
        {
            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var response = new Response<GetContentsByUserResponseModel>();
            try
            {
                var contents = await _contentFasade.GetContentByUserTokenAsync(jwt);

                if (!contents.Any()) //TODO: Comment?
                {
                    return NoContent();
                }

                _logger.LogInformation("Get content data finished properly.");
   
                response.Result = contents.ToGetContentsByUserResponseModel();
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.Message = ex.Message;
                return NotFound(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the content data.");
                response.Message = $"An error occurred while getting the content data. Error message: {ex.Message}";
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }


        [Authorize(Roles = "ContentCreator")]
        [HttpPost]
        public async Task<ActionResult> UploadContentAsync([FromBody] UploadContentRequestModel contentMetadata)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage);

                _logger.LogWarning("Invalid model state for UploadContentAsync: {Errors}", string.Join("; ", errorMessages));
                return BadRequest(ModelState);
            }

            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var response = new Response<bool>();

            try
            {
                await _contentFasade.UploadContentAsync(contentMetadata.ToContent(), jwt);

                _logger.LogInformation("Content data saved successfully.");

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
                _logger.LogError(ex, "An error occurred while adding content metadata.");
                return StatusCode(500, $"An error occurred while adding content metadata. Error message: {ex.Message}");
            }
        }

        [Authorize(Roles = "ContentCreator")]
        [HttpDelete]
        public async Task<ActionResult<string>> DeleteContentAsync(Guid contentId)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage);

                _logger.LogWarning("Invalid model state for DeleteContentAsync: {Errors}", string.Join("; ", errorMessages));
                return BadRequest(ModelState);
            }

            var response = new Response<bool>();

            try
            {
                await _contentFasade.DeleteContentAsync(contentId);

                _logger.LogInformation("Content metadata has been removed successfully.");
                response.Message = $"Content metadata has been removed successfully.";
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
                _logger.LogError(ex, "An error occurred while removing content metadata."); //TODO: test ex!
                response.Message = $"An error occurred while removing content metadata. Error message: {ex.Message}";
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [Authorize(Roles = "ContentCreator")]
        [HttpPut]
        public async Task<ActionResult<string>> EditContentAsync(Guid contentId, [FromBody] UploadContentRequestModel requestData)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage);

                _logger.LogWarning("Invalid model state for EditContentAsync: {Errors}", string.Join("; ", errorMessages));
                return BadRequest(ModelState);
            }

            var response = new Response<bool>();

            try
            {
                await _contentFasade.EditContentAsync(contentId, requestData.ToContent());

                _logger.LogInformation("Content metadata has been updated successfully.");
                response.Message = $"Content metadata has been updated successfully.";
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
                _logger.LogError(ex, "An error occurred while removing content metadata."); //TODO: test ex!
                response.Message = $"An error occurred while removing content metadata. Error message: {ex.Message}";
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
    }
}
