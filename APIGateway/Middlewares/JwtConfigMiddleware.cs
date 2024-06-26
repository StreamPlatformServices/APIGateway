using APIGatewayControllers.Middlewares.Attributes;
using APIGatewayRouting.IntegrationContracts;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json;

namespace APIGatewayControllers.Middlewares
{
    public class JwtConfigMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthorizationContract _authorizationContract;
        private static RsaSecurityKey _rsaSecurityKey;

        public JwtConfigMiddleware(RequestDelegate next, IAuthorizationContract authorizationContract)
        {
            _next = next;
            _authorizationContract = authorizationContract;

            //TODO: Commented for testing 
            //UpdateRsaSecurityPublicKey().GetAwaiter().GetResult(); //TODO: GetAwaiter ????
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                var updateJwtPublicKeyAttribute = endpoint.Metadata.GetMetadata<UpdateJwtPublicKey>();
                if (updateJwtPublicKeyAttribute != null)
                {
                    await UpdateRsaSecurityPublicKey();
                }
            }

            await _next(context);
        }

        public static RsaSecurityKey GetUpdatedRsaSecurityKey()
        {
            return _rsaSecurityKey;
        }

        private async Task UpdateRsaSecurityPublicKey()
        {
            var rsa = RSA.Create();

            var tokenValidationKey = await _authorizationContract.GetTokenPublicKey(); //TODO: Async????

            //TODO: if null or empty

            rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(tokenValidationKey), out _);

            _rsaSecurityKey = new RsaSecurityKey(rsa);
        }
    }
}
