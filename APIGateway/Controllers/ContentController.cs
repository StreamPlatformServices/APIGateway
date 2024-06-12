﻿using APIGatewayController.Models;
using Microsoft.AspNetCore.Mvc;
using APIGatewayRouting.Routing.Interfaces;
using APIGatewayControllers.DataMappers;
using APIGatewayControllers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

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


        //TODO: Add searching (searching will be aplicable on frontend side) 
        [HttpGet("GetAll", Name = "GetContents")]
        public async Task<ActionResult<IEnumerable<ContentModel>>> GetContentsAsync([FromQuery] int limit, [FromQuery] int offset)
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

                return Ok(content.Select(x => x.ToContentModel()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the content data.");
                return StatusCode(500, $"An error occurred while getting the content data. Error message: {ex.Message}");
            }
        }

        //TODO: Add endpoint for get contents by name 


        [Authorize(Roles = "ContentCreator")]
        [HttpPost("Upload", Name = "UploadContent")]
        public async Task<ActionResult<string>> UploadContentAsync([FromBody] ContentWithLicenseModel contentAndLicense)
        {
            //TODO: implement validators witch recognize BadRequest(). Think if the second validation is needed in Routing component!!

            try
            {
                bool result = await _contentRouter.UploadContentAsync(contentAndLicense.ContentModel.ToContent(), contentAndLicense.LicenseModel.ToContentLicense());
                if (result)
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
        [Authorize(Roles = "ContentCreator")]
        [HttpDelete("Delete", Name = "DeleteContent")]
        public async Task<ActionResult<string>> DeleteContentAsync([FromQuery] string contentName)
        {
            //TODO: Json parsing overriding 
            try
            {
                var content = await _contentRouter.GetContentByNameAsync(contentName);
                if (content == null)
                {
                    return NotFound($"Content: {contentName} doesn't exist!");
                }

                bool result = await _contentRouter.DeleteContentAsync(content.Uuid);
                if (!result)
                {
                    return StatusCode(500, $"Operation delete content can not be completed at the moment. Pleas try again later or contact the administrator for more information.");
                }

                return Ok($"{contentName} has been removed successfully.");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while removing the content.");
                return StatusCode(500, $"An error occurred while removing the content. Error message: {ex.Message}");
            }
        }

        [Authorize(Roles = "ContentCreator")]
        [HttpPut("Edit", Name = "EditContent")]
        public async Task<ActionResult<string>> EditContentAsync([FromQuery] string contentName, [FromBody] ContentModel contentModel)
        {
            //TODO: implement validators which recognize BadRequest()
            try
            {
                var content = await _contentRouter.GetContentByNameAsync(contentName);
                if (content == null)
                {
                    return NotFound($"Content: {contentName} doesn't exist!");
                }

                bool result = await _contentRouter.EditContentAsync(content.Uuid, contentModel.ToContent());
                if (!result)
                {
                    return StatusCode(500, $"Operation edit  can not be completed at the moment. Pleas try again later or contact the administrator for more information.");
                }

                return Ok($"{contentName} has been updated successfully.");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while editing the content.");
                return StatusCode(500, $"An error occurred while editing the content. Error message: {ex.Message}");
            }
        }

    }
}
