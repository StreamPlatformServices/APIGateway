using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using APIGatewayEntities.IntegrationContracts;
using APIGatewayControllers.Models.Responses;
using APIGatewayCoreUtilities.CommonExceptions;
using System.Net;
using StreamGatewayControllers.Models;
using APIGatewayControllers.DataMappers;
using APIGatewayEntities.Entities;

//TODO: Czy wogle powinna byc mozliwosc otrzymania linka ze streamem dla userow bez licencji????
namespace APIGateway.Controllers
{
    [ApiController]
    [Route("uri")]
    public class StreamUriController : ControllerBase
    {
        private readonly ILogger<StreamUriController> _logger;
        private readonly IStreamUriContract _streamUriContract;
        private readonly IContentMetadataContract _contentMetadataContract;

        public StreamUriController(
            ILogger<StreamUriController> logger,
            IStreamUriContract streamUriContract,
            IContentMetadataContract contentMetadataContract)
        {
            _logger = logger;
            _streamUriContract = streamUriContract;
            _contentMetadataContract = contentMetadataContract;
        }

        [HttpGet("video/{contentId}")]
        public async Task<IActionResult> GetVideoStreamUriAsync(Guid contentId) 
        {
            _logger.LogInformation($"Start get uri procedure for content: {contentId}");
            var response = new Response<GetUriResponseModel> { Result = new GetUriResponseModel() };

            try
            {
                var contentMetadata = await _contentMetadataContract.GetContentMetadataByIdAsync(contentId);

                var uriData = await _streamUriContract.GetVideoStreamUriAsync(contentMetadata.VideoFileId);

                response.Result = uriData.ToGetResponseModel();

                _logger.LogInformation($"Get uri procedure finished successfully content: {contentId}");
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.Message = ex.Message;
                return NotFound(response);
            }
            catch (UnauthorizedException ex) //TODO: handle token and authorization in StreamGateway. Not sure if token will be needed for get uri but probably yes
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
                _logger.LogError(ex, "An error occurred while getting the stream url.");
                response.Message = $"An error occurred while getting the stream url. Error message: {ex.Message}";
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("video")]
        public async Task<IActionResult> GetVideUploadUriAsync()
        {
            _logger.LogInformation("Start get video upload uri procedure.");
            var response = new Response<GetUriResponseModel> { Result = new GetUriResponseModel() };

            //try
            //{
            //    await _contentMetadataContract.GetContentMetadataByIdAsync(contentId);
            //}
            //catch (NotFoundException ex)
            //{
            //    response.Message = ex.Message;
            //    return NotFound(response);
            //}

            var videoFileId = Guid.NewGuid();
            //TODO:* ADD VIDEO FILE ID TO FILE RETENTION TABLE 

            try
            {
                var uriData = await _streamUriContract.GetVideoStreamUriAsync(videoFileId);

                response.Result = uriData.ToGetResponseModel();

                _logger.LogInformation($"Get video upload uri procedure finished successfully fileId: {videoFileId}");
                return Ok(response);
            }
            catch (UnauthorizedException ex) //TODO: handle token and authorization in StreamGateway. Not sure if token will be needed for get uri but probably yes
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
                _logger.LogError(ex, "An error occurred while getting the video upload url.");
                response.Message = $"An error occurred while getting the video upload url. Error message: {ex.Message}";
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }


        [HttpGet("image")]
        public async Task<IActionResult> GetImageUploadUriAsync()
        {
            _logger.LogInformation("Start get image upload uri procedure.");
            var response = new Response<GetUriResponseModel> { Result = new GetUriResponseModel() };

            //try
            //{
            //    await _contentMetadataContract.GetContentMetadataByIdAsync(contentId);
            //}
            //catch (NotFoundException ex)
            //{
            //    response.Message = ex.Message;
            //    return NotFound(response);
            //}

            var imageFileId = Guid.NewGuid();
            //TODO:* ADD IMAGE FILE ID TO FILE RETENTION TABLE 

            try
            {
                var uriData = await _streamUriContract.GetImageStreamUriAsync(imageFileId);

                response.Result = uriData.ToGetResponseModel();

                _logger.LogInformation($"Get image upload uri procedure finished successfully fileId: {imageFileId}");
                return Ok(response);
            }
            catch (NotFoundException ex) //TODO: Probably remove??
            {
                response.Message = ex.Message;
                return NotFound(response);
            }
            catch (UnauthorizedException ex) //TODO: handle token and authorization in StreamGateway. Not sure if token will be needed for get uri but probably yes
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
                _logger.LogError(ex, "An error occurred while getting the image upload url.");
                response.Message = $"An error occurred while getting the image upload url. Error message: {ex.Message}";
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

    }
}
