using APIGatewayRouting.Data;
using APIGatewayRouting.IntegrationContracts;
using AuthorizationServiceAPI.Models;
using Newtonsoft.Json;
using System.Text;

namespace AuthorizationServiceAPI
{
    public class AuthorizationContract : IAuthorizationContract 
    {

        private readonly HttpClient _httpClient;
        private const string SIGN_IN_ENDPOINT = "api/login";
        private const string GET_JWT_PUBLIC_KEY_ENDPOINT = "api/publickey";

        public AuthorizationContract(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //TODO: NOW!
        //TODO: Return TOKEN and test!!! 

        public async Task<string> AuthorizeAsync(string email, string password)
        {
            //TODO: Handle exception
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(new { email, password }),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(SIGN_IN_ENDPOINT, requestContent);

            if (!response.IsSuccessStatusCode)
            {
                //Log: $"SignIn failed: {response.ReasonPhrase}
                //throw new HttpRequestException($"SignIn failed: {response.ReasonPhrase}");
                //TODO: Throw or return false???
                return "";    
            }
            
            var responseContent = await response.Content.ReadAsStringAsync();
            var signInResponse = JsonConvert.DeserializeObject<ResponseModel<LoginResponseDto>>(responseContent);

            return signInResponse.Result.Token;
        }

        public async Task<string> GetTokenPublicKey()
        {
            var response = await _httpClient.GetAsync(GET_JWT_PUBLIC_KEY_ENDPOINT);
            if (!response.IsSuccessStatusCode)
            {
                //Log: $"SignIn failed: {response.ReasonPhrase}
                //throw new HttpRequestException($"SignIn failed: {response.ReasonPhrase}");
                //TODO: Throw or return false???
                return "";
            }

            var responseContent = await response.Content.ReadAsStringAsync(); 
            var jwtPublicKeyResponse = JsonConvert.DeserializeObject<ResponseModel<string>>(responseContent);
            return jwtPublicKeyResponse.Result;

        }
    }
}
