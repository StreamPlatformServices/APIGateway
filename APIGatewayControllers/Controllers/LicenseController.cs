using Microsoft.AspNetCore.Mvc;
using APIGatewayControllers.DataMappers;
using Microsoft.Extensions.Logging;
using APIGatewayEntities.IntegrationContracts;
using APIGatewayControllers.Models.Responses;
using APIGatewayCoreUtilities.CommonExceptions;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using APIGatewayControllers.Models.Requests.Comment;
using APIGatewayControllers.Models;
using APIGatewayEntities.Entities;
using APIGatewayEntities.Helpers.Interfaces;

namespace APIGateway.Controllers
{
    //TODO: Log start operations of endpoints!!!!!!!!!!!!!!!!!!!!
    [ApiController]
    [Route("license")]
    public class LicenseController : ControllerBase //TODO: NOW!!!!!!!!!!!! Create license functionality!!!!!!!!!!!!!!!!! 
    {
        private readonly ILogger<LicenseController> _logger;
        private readonly ILicenseAdapter _licenseAdapter;

        public LicenseController(
            ILogger<LicenseController> logger,
            ILicenseAdapter licenseAdapter)
        {
            _logger = logger;
            _licenseAdapter = licenseAdapter;
        }

        //TODO: ReqData ResponseData
        //[Authorize]
        [HttpGet("{contentId}")]
        public async Task<IActionResult> GetLicenseAsync(Guid contentId)
        {
            _logger.LogInformation($"Start get license data procedure.");

            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var response = new Response<LicenseResponseModel> { Result = new LicenseResponseModel() };
            try
            {
                var license = await _licenseAdapter.GetLicenseAsync(contentId, jwt);

                _logger.LogInformation($"Get license data finished successfully.");
                response.Message = $"Get license data finished successfully.";
                response.Result = license.ToContentLicenseModel();
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
                _logger.LogError(ex, "An error occurred while getting license data.");
                response.Message = $"An error occurred while getting license data. Error message: {ex.Message}";
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> IssueLicenseAsync([FromBody] LicenseRequestModel licenseModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage);

                _logger.LogWarning("Invalid model state for IssueLicenseAsync: {Errors}", string.Join("; ", errorMessages));
                return BadRequest(ModelState);
            }

            _logger.LogInformation($"Start issue license data procedure.");

            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var response = new Response<bool> ();
            try
            {
                await _licenseAdapter.IssueLicenseAsync(licenseModel.ToContentLicense(), jwt);

                _logger.LogInformation($"Issue license finished successfully.");
                response.Message = $"Issue license finished successfully.";
                response.Result = true;
                return Ok(response);
            }
            catch (ConflictException ex)
            {
                response.Message = ex.Message;
                response.Result = false;
                return Conflict(response);
            }
            catch (UnauthorizedException ex)
            {
                response.Message = ex.Message;
                response.Result = false;
                return Unauthorized(response);
            }
            catch (ForbiddenException ex)
            {
                response.Message = ex.Message;
                response.Result = false;
                return StatusCode((int)HttpStatusCode.Forbidden, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while issueing license data.");
                response.Message = $"An error occurred while issueing license data. Error message: {ex.Message}";
                response.Result = false;
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        //[Authorize]
        [HttpPut]
        public async Task<ActionResult<string>> ExtendLicenseAsync([FromBody] LicenseRequestModel licenseModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage);

                _logger.LogWarning("Invalid model state for ExtendLicenseAsync: {Errors}", string.Join("; ", errorMessages));
                return BadRequest(ModelState);
            }

            _logger.LogInformation($"Start extend license data procedure.");

            string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var response = new Response<bool>();
            try
            {
                await _licenseAdapter.ExtendLicenseAsync(licenseModel.ToContentLicense(), jwt);

                _logger.LogInformation($"Extend license finished successfully.");
                response.Message = $"Extend license finished successfully.";
                response.Result = true;
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.Message = ex.Message;
                response.Result = false;
                return NotFound(response);
            }
            catch (UnauthorizedException ex)
            {
                response.Message = ex.Message;
                response.Result = false;
                return Unauthorized(response);
            }
            catch (ForbiddenException ex)
            {
                response.Message = ex.Message;
                response.Result = false;
                return StatusCode((int)HttpStatusCode.Forbidden, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while extending license data.");
                response.Message = $"An error occurred while extending license data. Error message: {ex.Message}";
                response.Result = false;
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
    }

}
