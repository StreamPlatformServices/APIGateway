using APIGatewayRouting.Data;
using APIGatewayRouting.IntegrationContracts;
using AuthorizationServiceAPI.DataMappers;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using AuthorizationServiceAPI.Models.Responses;
using APIGatewayCoreUtilities.CommonExceptions;
using System.Net;
using Microsoft.Extensions.Logging;

namespace AuthorizationServiceAPI
{
    public class UserContract : IUserContract //TODO: Change name to UserService??
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserContract> _logger;

        private const string USERS_ENDPOINT = "users";
        private const string GET_USER_ENDPOINT = "user"; 
        private const string CONTENT_CREATOR_ENDPOINT = "content-creator";
        private const string END_USER_ENDPOINT = "end-user";
        private const string USER_STATUS_ENDPOINT = "status";

        public UserContract(
            ILogger<UserContract> logger, 
            HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        async Task<IEnumerable<User>> IUserContract.GetAllUsersAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            try
            {
                var response = await _httpClient.GetAsync(USERS_ENDPOINT);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var userResponse = JsonConvert.DeserializeObject<ResponseModel<UsersResponseDto>>(responseContent);

                    var users = userResponse?.Result?.Users;
                    if (users == null)
                    {
                        _logger.LogError("Deserialized user object is empty.");
                        throw new Exception("User object is empty after deserialization");
                    }

                    return users.ToUsers();
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        _logger.LogWarning("Request should be blocked in APIGateway middleware!"); //TODO: log error???
                        throw new UnauthorizedException(response.ReasonPhrase);
                    case HttpStatusCode.Forbidden:
                        _logger.LogWarning("Request should be blocked in APIGateway middleware!");
                        throw new ForbiddenException(response.ReasonPhrase);
                    default:
                        _logger.LogError($"Unexpected error in response while trying to get all users. Message: {response.ReasonPhrase}");
                        throw new Exception(response.ReasonPhrase);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request error occurred while trying to get all users.");
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error occurred while trying to get all users.");
                throw;
            }
        }

        async Task<User> IUserContract.GetUserAsync(string token) 
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var response = await _httpClient.GetAsync($"{USERS_ENDPOINT}/{GET_USER_ENDPOINT}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var userResponse = JsonConvert.DeserializeObject<ResponseModel<UserResponseDto>>(responseContent);
                    var user = userResponse?.Result;

                    if (user == null)
                    {
                        _logger.LogError("Deserialized user object is empty.");
                        throw new Exception("User object is empty after deserialization");
                    }

                    return user.ToUser();
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        throw new NotFoundException(response.ReasonPhrase);
                    case HttpStatusCode.Unauthorized:
                        _logger.LogWarning("Request should be blocked in APIGateway middleware!"); 
                        throw new UnauthorizedException(response.ReasonPhrase);
                    case HttpStatusCode.Forbidden:
                        _logger.LogWarning("Request should be blocked in APIGateway middleware!");
                        throw new ForbiddenException(response.ReasonPhrase);
                    default:
                        _logger.LogError($"Unexpected error in response while trying to get user data. Message: {response.ReasonPhrase}");
                        throw new Exception(response.ReasonPhrase);
                }
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

        async Task<bool> IUserContract.AddContentCreatorUserAsync(ContentCreatorUser user)
        {
            try 
            { 
                var requestContent = new StringContent(
                    JsonConvert.SerializeObject(user.ToUserRequestDto()),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync($"{USERS_ENDPOINT}/{CONTENT_CREATOR_ENDPOINT}", requestContent);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new ConflictException(response.ReasonPhrase);
                }
                
                _logger.LogError($"Unexpected error in response while trying to add content creator user. Message: {response.ReasonPhrase}");
                throw new Exception($"{response.ReasonPhrase}");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request error occurred while trying to add content creator user.");
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error occurred while trying to add content creator user.");
                throw;
            }
        }
        async Task<bool> IUserContract.AddEndUserAsync(EndUser user)
        {
            try
            { 
                var requestContent = new StringContent(
                    JsonConvert.SerializeObject(user.ToUserRequestDto()),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync($"{USERS_ENDPOINT}/{END_USER_ENDPOINT}", requestContent);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new ConflictException(response.ReasonPhrase);
                }

                _logger.LogError($"Unexpected error in response while trying to add end user. Message: {response.ReasonPhrase}");
                throw new Exception($"{response.ReasonPhrase}");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request error occurred while trying to add end user.");
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error occurred while trying to add end user.");
                throw;
            }
        }

        async Task<bool> IUserContract.EditContentCreatorUserAsync(ContentCreatorUser user, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {     
                var requestContent = new StringContent(
                    JsonConvert.SerializeObject(user.ToUserRequestDto()),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PutAsync($"{USERS_ENDPOINT}/{CONTENT_CREATOR_ENDPOINT}", requestContent);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        throw new NotFoundException(response.ReasonPhrase);
                    case HttpStatusCode.Unauthorized:
                        _logger.LogWarning("Request should be blocked in APIGateway middleware!");
                        throw new UnauthorizedException(response.ReasonPhrase);
                    case HttpStatusCode.Forbidden:
                        _logger.LogWarning("Request should be blocked in APIGateway middleware!");
                        throw new ForbiddenException(response.ReasonPhrase);
                    default:
                        _logger.LogError($"Unexpected error in response while trying to update content creator user. Message: {response.ReasonPhrase}");
                        throw new Exception(response.ReasonPhrase);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request error occurred while trying to update end user.");
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error occurred while trying to update end user.");
                throw;
            }
        }
        async Task<bool> IUserContract.EditEndUserAsync(EndUser user, string token) //TODO: change return type to void 
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {    
                var requestContent = new StringContent(
                    JsonConvert.SerializeObject(user.ToUserRequestDto()),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PutAsync($"{USERS_ENDPOINT}/{END_USER_ENDPOINT}", requestContent);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        throw new NotFoundException(response.ReasonPhrase);
                    case HttpStatusCode.Unauthorized:
                        _logger.LogWarning("Request should be blocked in APIGateway middleware!");
                        throw new UnauthorizedException(response.ReasonPhrase);
                    case HttpStatusCode.Forbidden:
                        _logger.LogWarning("Request should be blocked in APIGateway middleware!");
                        throw new ForbiddenException(response.ReasonPhrase);
                    default:
                        _logger.LogError($"Unexpected error in response while trying to update end user. Message: {response.ReasonPhrase}");
                        throw new Exception(response.ReasonPhrase);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request error occurred while trying to update end user.");
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error occurred while trying to update end user.");
                throw;
            }
        }
        async Task<bool> IUserContract.RemoveUserAsync(string password, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var requestContent = new StringContent(
                    JsonConvert.SerializeObject(new { password }),
                    Encoding.UTF8,
                    "application/json");

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(_httpClient.BaseAddress, USERS_ENDPOINT),
                    Content = requestContent
                };

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        throw new NotFoundException(response.ReasonPhrase);
                    case HttpStatusCode.Unauthorized:
                        _logger.LogWarning("Request should be blocked in APIGateway middleware!");
                        throw new UnauthorizedException(response.ReasonPhrase);
                    case HttpStatusCode.Forbidden:
                        _logger.LogWarning("Request should be blocked in APIGateway middleware!");
                        throw new ForbiddenException(response.ReasonPhrase);
                    default:
                        _logger.LogError($"Unexpected error in response while trying to remove user. Message: {response.ReasonPhrase}");
                        throw new Exception(response.ReasonPhrase);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request error occurred while trying to remove user.");
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error occurred while trying to remove user.");
                throw;
            }
        }

        async Task<bool> IUserContract.ChangeUserStatusAsync(string userName, bool status, string token) //TODO: Change userName to uuid?
        {
            var isActive = status;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var requestContent = new StringContent(
                    JsonConvert.SerializeObject(new { isActive }),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PatchAsync($"{USERS_ENDPOINT}/{userName}/{USER_STATUS_ENDPOINT}", requestContent);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        throw new NotFoundException(response.ReasonPhrase);
                    case HttpStatusCode.Unauthorized:
                        _logger.LogWarning("Request should be blocked in APIGateway middleware!");
                        throw new UnauthorizedException(response.ReasonPhrase);
                    case HttpStatusCode.Forbidden:
                        _logger.LogWarning("Request should be blocked in APIGateway middleware!");
                        throw new ForbiddenException(response.ReasonPhrase);
                    default:
                        _logger.LogError($"Unexpected error in response while trying to change user status. Message: {response.ReasonPhrase}");
                        throw new Exception(response.ReasonPhrase);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request error occurred while trying to change user status.");
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error occurred while trying to change user status.");
                throw;
            }
        }
    }
}
