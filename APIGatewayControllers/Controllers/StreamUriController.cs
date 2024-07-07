using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using APIGatewayEntities.IntegrationContracts;


namespace APIGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StreamUriController : ControllerBase
    {
        private readonly ILogger<StreamUriController> _logger;
        private readonly IStreamUriContract _streamUriContract;

        public StreamUriController(
            ILogger<StreamUriController> logger,
            IStreamUriContract streamUriContract)
        {
            _logger = logger;
            _streamUriContract = streamUriContract;
        }

        [Authorize]
        [HttpGet("GetStreamUrl", Name = "GetStreamUrl")]
        public async Task<ActionResult<string>> GetStreamUriAsync(Guid contentName) //TODO: Request data
        {
            //TODO: Create JSON model for URI ????????!!!!!!!!!!!!!!!
            //TODO: Information if it is once or infinit!!!!!!!!!!!

            try
            {
                var streamUri = await _streamUriContract.GetStreamUriAsync(contentName);
                if (streamUri == null)
                {
                    return StatusCode(500, $"Operation get stream url can not be completed at the moment. Pleas try again later or contact the administrator for more information.");
                }

                return Ok(streamUri);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the stream url.");
                return StatusCode(500, $"An error occurred while getting the stream url. Error message: {ex.Message}");
            }
        }

        [HttpGet("GetUploadUri", Name = "GetUploadUri")]
        public async Task<ActionResult<string>> GetUploadUriAsync(Guid contentName)
        {
            //TODO: Create JSON model for URI ????????!!!!!!!!!!!!!!!
            //TODO: Information if it is once or infinit!!!!!!!!!!!

            try
            {
                var streamUri = await _streamUriContract.GetUploadUriAsync(contentName);
                if (streamUri == null)
                {
                    return StatusCode(500, $"Operation get upload url can not be completed at the moment. Pleas try again later or contact the administrator for more information.");
                }

                return Ok(streamUri);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the upload url.");
                return StatusCode(500, $"An error occurred while getting the upload url. Error message: {ex.Message}");
            }
        }

    }
}
