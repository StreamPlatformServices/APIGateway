using APIGatewayRouting.Data;
using APIGatewayRouting.IntegrationContracts;
using AuthorizationServiceAPI.DataMappers;
using AuthorizationServiceAPI.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

namespace AuthorizationServiceAPI
{
    public class UserContract : IUserContract //TODO: Change name to UserService
    {

        private readonly HttpClient _httpClient;

        private const string GET_USERS_ENDPOINT = "api/users";

        public UserContract(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        

        async Task<bool> IUserContract.AddContentCreatorUserAsync(ContentCreatorUser user)
        {
            throw new NotImplementedException();
        }
        async Task<User> IUserContract.GetUserByNameAsync(string userName, string token)
        {
            //TODO: change to GetUser(token) and the user will be recognized by id from token

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(GET_USERS_ENDPOINT);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var usersResponse = JsonConvert.DeserializeObject<ResponseModel<UsersResponseDto>>(responseContent);

                //var users = new List<User>();

                foreach (var user in usersResponse.Result.Users)
                {
                    if (user.Name == userName)
                    {
                        var role = user.Roles.FirstOrDefault();
                        //if (string.IsNullOrEmpty(role))
                        //{
                        //    //TODO: log
                        //    return null;
                        //}

                        //if (role == "EndUser")
                        //{
                        //    return user.ToContentCreatorUser();
                        //}

                        //if (role == "ContentCreatorUser")
                        //{
                            return user.ToEndUser();
                        //}

                        //return null; //TODO: or throw???
                    }
                }

                return null;
            }
            else
            {
                // Obsługa błędów
                throw new HttpRequestException($"SignIn failed: {response.ReasonPhrase}");
            }
        }
        async Task<bool> IUserContract.AddEndUserAsync(EndUser user)
        {
            throw new NotImplementedException();
        }
        async Task<bool> IUserContract.EditContentCreatorUserAsync(Guid userId, ContentCreatorUser user, string token)
        {
            throw new NotImplementedException();
        }
        async Task<bool> IUserContract.EditEndUserAsync(Guid userId, EndUser user, string token)
        {
            throw new NotImplementedException();
        }
        async Task<bool> IUserContract.RemoveUserAsync(Guid userId, string token)
        {
            throw new NotImplementedException();
        }
    }
}
