using APIGatewayCoreUtilities.CommonExceptions;
using APIGatewayRouting.IntegrationContracts;
using AuthorizationServiceAPI.Models.Responses;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace AuthorizationServiceAPI //TODO: Log.Information, Log.Debug
{
    public class AuthorizationContract : IAuthorizationContract 
    {
        private readonly ILogger<UserContract> _logger;
        private readonly HttpClient _httpClient;
        private const string AUTHORIZATION_ENDPOINT = "auth";
        private const string SIGN_IN_ENDPOINT = "login";
        private const string JWT_PUBLIC_KEY_ENDPOINT = "publickey";

        public AuthorizationContract(
            ILogger<UserContract> logger,
            HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<string> AuthorizeAsync(string email, string password)
        {
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(new { email, password }),
                Encoding.UTF8,
                "application/json");

            try
            {
                var response = await _httpClient.PostAsync($"{AUTHORIZATION_ENDPOINT}/{SIGN_IN_ENDPOINT}", requestContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var signInResponse = JsonConvert.DeserializeObject<ResponseModel<AuthorizeResponseDto>>(responseContent);

                    var token = signInResponse?.Result?.Token;
                    if (token == null)
                    {
                        _logger.LogError("Deserialized token object is empty.");
                        throw new Exception("Token object is empty after deserialization");
                    }

                    return token;
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        _logger.LogError($"Authorization failed. Error message: {response.ReasonPhrase}");
                        throw new UnauthorizedException(response.ReasonPhrase);
                    case HttpStatusCode.Forbidden:
                        _logger.LogError($"Authorization failed. Error message: {response.ReasonPhrase}");
                        throw new ForbiddenException(response.ReasonPhrase);
                    default:
                        _logger.LogError($"Unexpected error in response message: {response.ReasonPhrase}");
                        throw new Exception(response.ReasonPhrase);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request error occurred while trying to authorize.");
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error occurred while trying to authorize.");
                throw;
            }         
        }

        public async Task<string> GetTokenPublicKey()
        {
            try 
            { 
                var response = await _httpClient.GetAsync($"{AUTHORIZATION_ENDPOINT}/{JWT_PUBLIC_KEY_ENDPOINT}");
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var jwtPublicKeyResponse = JsonConvert.DeserializeObject<ResponseModel<string>>(responseContent);

                    var jwtPublicKey = jwtPublicKeyResponse?.Result;
                    if (jwtPublicKey == null)
                    {
                        _logger.LogError($"Failed to get JWT public key. Key is empty after deserialization. Respnonse reason phrase: {response.ReasonPhrase}");
                        throw new Exception("JWT public key is empty after deserialization");
                    }

                    return jwtPublicKey; 
                }


                _logger.LogError($"Failed to get JWT public key. Respnonse reason phrase: {response.ReasonPhrase}");
                throw new UnauthorizedException($"{response.ReasonPhrase}");

            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request error occurred while trying to get JWT public key.");
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error occurred while trying to get JWT public key.");
                throw;
            }  
        }
    }
}
