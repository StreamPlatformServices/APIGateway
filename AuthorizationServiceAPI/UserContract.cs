using APIGatewayRouting.Data;
using APIGatewayRouting.IntegrationContracts;
using AuthorizationServiceAPI.DataMappers;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;
using AuthorizationServiceAPI.Models.Responses;

namespace AuthorizationServiceAPI
{
    public class UserContract : IUserContract //TODO: Change name to UserService
    {

        private readonly HttpClient _httpClient;

        private const string USERS_ENDPOINT = "users";
        private const string GET_USER_ENDPOINT = "user"; 
        private const string CONTENT_CREATOR_ENDPOINT = "content-creator";
        private const string END_USER_ENDPOINT = "end-user";
        private const string USER_STATUS_ENDPOINT = "status";

        public UserContract(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        async Task<IEnumerable<User>> IUserContract.GetAllUsersAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(USERS_ENDPOINT);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var userResponse = JsonConvert.DeserializeObject<ResponseModel<UsersResponseDto>>(responseContent);

                return userResponse.Result.Users.ToUsers();
            }
            else
            {
                // Obsługa błędów
                throw new HttpRequestException($"SignIn failed: {response.ReasonPhrase}");
            }
        }

        async Task<User> IUserContract.GetUserAsync(string token) 
        {
            //TODO: test
            //TODO: Remove login and password from model. Change model 

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{USERS_ENDPOINT}/{GET_USER_ENDPOINT}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var userResponse = JsonConvert.DeserializeObject<ResponseModel<UserResponseDto>>(responseContent);
                var user = userResponse.Result;

                return user.ToUser();
            }
            else
            {
                // Obsługa błędów
                throw new HttpRequestException($"SignIn failed: {response.ReasonPhrase}");
            }
        }

        async Task<bool> IUserContract.AddContentCreatorUserAsync(ContentCreatorUser user)
        {
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(user.ToUserRequestDto()),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync($"{USERS_ENDPOINT}/{CONTENT_CREATOR_ENDPOINT}", requestContent);

            return response.IsSuccessStatusCode;

            //TODO: exception handling!
        }
        async Task<bool> IUserContract.AddEndUserAsync(EndUser user)
        {
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(user.ToUserRequestDto()),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync($"{USERS_ENDPOINT}/{END_USER_ENDPOINT}", requestContent);

            return response.IsSuccessStatusCode;

            //TODO: exception handling!
        }

        async Task<bool> IUserContract.EditContentCreatorUserAsync(ContentCreatorUser user, string token) //TODO: Remove user id
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(user.ToUserRequestDto()),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PutAsync($"{USERS_ENDPOINT}/{CONTENT_CREATOR_ENDPOINT}", requestContent);

            return response.IsSuccessStatusCode;

            //TODO: exception handling!
        }
        async Task<bool> IUserContract.EditEndUserAsync(EndUser user, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(user.ToUserRequestDto()),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PutAsync($"{USERS_ENDPOINT}/{END_USER_ENDPOINT}", requestContent);

            return response.IsSuccessStatusCode;

            //TODO: exception handling!
        }
        async Task<bool> IUserContract.RemoveUserAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.DeleteAsync(USERS_ENDPOINT);

            return response.IsSuccessStatusCode;
        }

        async Task<bool> IUserContract.ChangeUserStatusAsync(string userName, bool status, string token) //TODO: Change userName to uuid?
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(status),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PatchAsync($"{USERS_ENDPOINT}/{userName}/{USER_STATUS_ENDPOINT}", requestContent);

            return response.IsSuccessStatusCode;
        }
    }
}
