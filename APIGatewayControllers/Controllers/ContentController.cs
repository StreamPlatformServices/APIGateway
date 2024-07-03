using Microsoft.AspNetCore.Mvc;
using APIGatewayRouting.Routing.Interfaces;
using APIGatewayControllers.DataMappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using APIGatewayControllers.Models.Requests;
using Microsoft.Extensions.Logging;

namespace APIGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContentController : ControllerBase
    {
        private readonly ILogger<ContentController> _logger;
        private readonly IContentRouter _contentRouter;

        public ContentController(
            ILogger<ContentController> logger,
            IContentRouter contentRouter)
        {
            _logger = logger;
            _contentRouter = contentRouter;
        }


        //TODO: Add searching (searching will be aplicable on frontend side??) 

        //TODO: Now get the 
        [HttpGet("GetAll", Name = "GetContents")]
        public async Task<ActionResult<IEnumerable<UploadContentRequestModel>>> GetContentsAsync([FromQuery] int limit, [FromQuery] int offset)
        {
            //TODO: Generate Snapshots 
            //TODO: Content cacher in routing component
            try
            {
                var content = await _contentRouter.GetAllContentsAsync(limit, offset);
                if (content == null || !content.Any())
                {
                    return NotFound();
                }

                return Ok(content.Select(x => x.ToGetAllContentsResponseModel()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the content data.");
                return StatusCode(500, $"An error occurred while getting the content data. Error message: {ex.Message}");
            }
        }

        [HttpGet("Get", Name = "GetContent")]
        public async Task<ActionResult<IEnumerable<UploadContentRequestModel>>> GetContentByIdAsync(Guid uuid)
        {
            try
            {
                var content = await _contentRouter.GetContentByIdAsync(uuid);
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
                bool result = await _contentRouter.UploadContentAsync(contentMetadata.ToContent());
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
                var content = await _contentRouter.GetContentByIdAsync(contentId);
                if (content == null)
                {
                    return NotFound($"Content with provided id doesn't exist!");
                }

                bool result = await _contentRouter.DeleteContentAsync(content.Uuid);
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
                var content = await _contentRouter.GetContentByIdAsync(contentId); //TODO: czy nie powinno to być przezroczyste?? 
                if (content == null)
                {
                    return NotFound($"Content with provided id doesn't exist!");
                }

                bool result = await _contentRouter.EditContentAsync(contentId, contentModel.ToContent());
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
