﻿using APIGatewayControllers.Middlewares.Attributes;
using APIGatewayCoreUtilities.CommonExceptions;
using APIGatewayEntities.IntegrationContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

using System.Security.Cryptography;


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
                    await UpdateRsaSecurityPublicKey(context);
                }
            }

            await _next(context);
        }

        public static RsaSecurityKey GetUpdatedRsaSecurityKey()
        {
            return _rsaSecurityKey;
        }

        private async Task UpdateRsaSecurityPublicKey(HttpContext context)
        {
            try
            {
                var rsa = RSA.Create();

                var tokenValidationKey = await _authorizationContract.GetTokenPublicKey(); //TODO: Async????

                if (string.IsNullOrEmpty(tokenValidationKey))
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync("JWT public key is null or empty.");
                    return;
                }

                rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(tokenValidationKey), out _);

                _rsaSecurityKey = new RsaSecurityKey(rsa);
            }
            catch (UnauthorizedException ex)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync($"Unauthorized access. Error message: {ex.Message}");
                return;
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync($"An error occurred while updating RSA security public key. Error message: {ex.Message}");
                return;
            }
        }
    }
}
