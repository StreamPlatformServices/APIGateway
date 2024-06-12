using APIGatewayRouting.IntegrationContracts;
using APIGatewayRouting.Routing;
using APIGatewayRouting.Routing.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StreamGatewayMock;
using System.Security.Cryptography;
using AuthorizationServiceAPI.ServiceCollectionExtensions;
using APIGatewayControllers.Configuration;
using APIGatewayControllers.ServiceCollectionExtensions;
using APIGatewayControllers.Middlewares;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var externalServicesSettings = builder.Configuration.GetSection("ExternalServicesCommunication").Get<ExternalServicesCommunicationSettings>();
//TODO: Move all extension methods to this component
//TODO: ExtractSettings inside the methods (no params needed)
builder.Services.AddAuthorizationServiceAPI("https://localhost:7124");//externalServicesSettings.AuthorizationServiceAPISettings.BaseUrl);
builder.Services.AddJWTConfiguration(externalServicesSettings.JwtSettings.Issuer, externalServicesSettings.JwtSettings.Audience);

builder.Services.AddTransient<IStreamUriContract, StreamGatewayContract>(); //TODO: How to not depend on StreamGatewayMock. It is not possible. We need the whole config in one place

builder.Services.AddTransient<IContentRouter, ContentRouter>();
builder.Services.AddTransient<IUserRouter, UserRouter>();
builder.Services.AddTransient<IContentCommentRouter, ContentCommentRouter>();
builder.Services.AddTransient<IStreamUriRouter, StreamUriRouter>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIGateway API", Version = "v1" });

    // Configure Bearer authorization
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter JWT token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<JwtConfigMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


