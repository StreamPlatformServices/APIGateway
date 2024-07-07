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

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UploadContentRequestModel>>> GetContentsAsync([FromQuery] int limit, [FromQuery] int offset)
        {
            //TODO: Generate Snapshots 
            //TODO: Content cacher in fasade???
            var response = new Response<IEnumerable<GetAllContentsResponseModel>>();

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
        public async Task<ActionResult<IEnumerable<UploadContentRequestModel>>> GetContentByIdAsync(Guid uuid)
        {
            try
            {
                var content = await _contentFasade.GetContentByIdAsync(uuid);
                if (content.Uuid == Guid.Empty)
                {
                    return NotFound();
                }

                return Ok(content.ToGetContentResponseModel());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the content data.");
                return StatusCode(500, $"An error occurred while getting the content data. Error message: {ex.Message}");
            }
        }


        //[Authorize(Roles = "ContentCreator")]
        [HttpPost("Upload", Name = "UploadContent")]
        public async Task<ActionResult<string>> UploadContentAsync([FromBody] UploadContentRequestModel contentMetadata)
        {
            //TODO: implement validators witch recognize BadRequest(). Think if the second validation is needed in Routing component!!

            try
            {
                bool result = await _contentFasade.UploadContentAsync(contentMetadata.ToContent());
                if (!result)
                {
                    return StatusCode(500, $"Operation upload content can not be completed at the moment. Pleas try again later or contact the administrator for more information.");
                }

                return Ok($" has been added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a user.");
                return StatusCode(500, $"An error occurred while adding a user. Error message: {ex.Message}");
            }
        }

        //TODO: Recheck logs and return messages
        //[Authorize(Roles = "ContentCreator")]
        [HttpDelete("Delete", Name = "DeleteContent")]
        public async Task<ActionResult<string>> DeleteContentAsync([FromBody] Guid contentId) //TODO: FromBody?????
        {
            //TODO: Json parsing overriding 
            try
            {
                var content = await _contentFasade.GetContentByIdAsync(contentId);
                if (content == null)
                {
                    return NotFound($"Content with provided id doesn't exist!");
                }

                bool result = await _contentFasade.DeleteContentAsync(content.Uuid);
                if (!result)
                {
                    return StatusCode(500, $"Operation delete content can not be completed at the moment. Pleas try again later or contact the administrator for more information.");
                }

                return Ok($"Content has been removed successfully.");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while removing the content.");
                return StatusCode(500, $"An error occurred while removing the content. Error message: {ex.Message}");
            }
        }

        //[Authorize(Roles = "ContentCreator")]
        [HttpPut("Edit", Name = "EditContent")]
        public async Task<ActionResult<string>> EditContentAsync([FromQuery] Guid contentId, [FromBody] UploadContentRequestModel contentModel)
        {
            //TODO: implement validators which recognize BadRequest()
            try
            {
                var content = await _contentFasade.GetContentByIdAsync(contentId); //TODO: czy nie powinno to być przezroczyste?? 
                if (content == null)
                {
                    return NotFound($"Content with provided id doesn't exist!");
                }

                bool result = await _contentFasade.EditContentAsync(contentId, contentModel.ToContent());
                if (!result)
                {
                    return StatusCode(500, $"Operation edit  can not be completed at the moment. Pleas try again later or contact the administrator for more information.");
                }

                return Ok($"Content has been updated successfully.");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while editing the content.");
                return StatusCode(500, $"An error occurred while editing the content. Error message: {ex.Message}");
            }
        }

    }
}
