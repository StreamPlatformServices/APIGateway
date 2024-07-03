using APIGatewayControllers.Middlewares;
using APIGatewayCoreUtilities.CommonConfiguration.ConfigurationModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace APIGatewayMain.ServiceCollectionExtensions
{
    public static class ControllersExtensions
    {
        public static IServiceCollection AddJWTConfiguration(this IServiceCollection services)
        {
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var jwtSettings = serviceProvider.GetRequiredService<IOptions<JwtSettings>>().Value;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
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
