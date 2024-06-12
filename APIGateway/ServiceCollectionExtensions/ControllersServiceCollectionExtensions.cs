using APIGatewayControllers.Middlewares;
using APIGatewayRouting.IntegrationContracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace APIGatewayControllers.ServiceCollectionExtensions
{
   
    public static class ControllersServiceCollectionExtensions
    {
        public static IServiceCollection AddJWTConfiguration(this IServiceCollection services, string issuer, string audiance)
        {
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audiance,
                    IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                    {
                        return new[] { JwtConfigMiddleware.GetUpdatedRsaSecurityKey() };
                    }
                };
            });

            return services;
        }
    }
    
}
