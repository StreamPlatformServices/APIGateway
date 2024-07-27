using APIGatewayCoreUtilities.CommonExceptions;
using APIGatewayEntities.IntegrationContracts;
using System.Net;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StreamGatewayAPI.Models.Responses;
using StreamGatewayControllers.Models;
using StreamGatewayAPI.DataMappers;

namespace StreamGatewayAPI
{
    public class StreamUriContract : IStreamUriContract
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<StreamUriContract> _logger;

        private const string URI_ENDPOINT = "uri";
        private const string VIDEO_ENDPOINT = "video";
        private const string IMAGE_ENDPOINT = "image";

        public StreamUriContract(
            ILogger<StreamUriContract> logger,
            HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }
        
        public async Task<UriData> GetVideoStreamUriAsync(Guid contentId)
        {
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); //TODO: Authorization on StreamGateway

            try
            {
                var response = await _httpClient.GetAsync($"{URI_ENDPOINT}/{VIDEO_ENDPOINT}/{contentId}"); 

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var uriResponse = JsonConvert.DeserializeObject<ResponseModel<GetUriResponseModel>>(responseContent); 

                    if (string.IsNullOrEmpty(uriResponse?.Result.Url))
                    {
                        _logger.LogError("Deserialized uri object is empty.");
                        throw new Exception("Uri object is empty after deserialization");
                    }

                    return uriResponse.Result.ToUriData();
                }

                HandleStatusCode(response);
                _logger.LogError($"Unexpected error in response while trying to get video stream uri. Message: {response.ReasonPhrase}");
                throw new Exception(response.ReasonPhrase);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request error occurred while trying to get user data.");
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error occurred while trying to get user data.");
                throw;
            }
        }

        public async Task<UriData> GetImageStreamUriAsync(Guid contentId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{URI_ENDPOINT}/{IMAGE_ENDPOINT}/{contentId}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var uriResponse = JsonConvert.DeserializeObject<ResponseModel<GetUriResponseModel>>(responseContent);

                    if (string.IsNullOrEmpty(uriResponse?.Result.Url))
                    {
                        _logger.LogError("Deserialized uri object is empty.");
                        throw new Exception("Uri object is empty after deserialization");
                    }

                    return uriResponse.Result.ToUriData();
                }

                HandleStatusCode(response);
                _logger.LogError($"Unexpected error in response while trying to get video stream uri. Message: {response.ReasonPhrase}");
                throw new Exception(response.ReasonPhrase);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request error occurred while trying get to video stream uri.");
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error occurred while trying to get video stream uri.");
                throw;
            }
        }

        private void HandleStatusCode(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound: //TODO: Is NotFound needed in this place
                    throw new NotFoundException(response.ReasonPhrase);
                case HttpStatusCode.Unauthorized:
                    _logger.LogWarning("Request should be blocked in APIGateway middleware!"); //TODO: should be autho in strem gateway???
                    throw new UnauthorizedException(response.ReasonPhrase);
                case HttpStatusCode.Forbidden:
                    _logger.LogWarning("Request should be blocked in APIGateway middleware!");
                    throw new ForbiddenException(response.ReasonPhrase);  
            }
        }
    }
}
