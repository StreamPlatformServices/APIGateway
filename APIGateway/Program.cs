using APIGatewayRouting.IntegrationContracts;
using APIGatewayRouting.Routing;
using APIGatewayRouting.Routing.Interfaces;
using StreamGatewayMock;
using AuthorizationServiceAPI.ServiceCollectionExtensions;
using ContentMetadataServiceMock.ServiceCollectionExtensions;
using APIGatewayControllers.ServiceCollectionExtensions;
using APIGatewayControllers.Middlewares;
using Microsoft.OpenApi.Models;
using ContentMetadataServiceMock;
using APIGatewayControllers.DTO.Models.Requests;
using APIGatewayControllers.Models.Requests;
using APIGatewayControllers.Validators;
using FluentValidation;

//TODO: Add all extension methods for serviceCollection to Main Component
//TODO: Separate Main Component from Controllers Component

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCommonConfiguration(builder.Configuration);
builder.Services.AddAuthorizationServiceAPI();
builder.Services.AddJWTConfiguration();
builder.Services.AddContentMetadataMock(); 

//TODO: Move to extension methods
builder.Services.AddTransient<IStreamUriContract, StreamGatewayContract>();
builder.Services.AddTransient<IContentMetadataContract, ContentMetadataContract>();

builder.Services.AddTransient<IContentRouter, ContentRouter>();
builder.Services.AddTransient<IUserRouter, UserRouter>();
builder.Services.AddTransient<IContentCommentRouter, ContentCommentRouter>();
builder.Services.AddTransient<IStreamUriRouter, StreamUriRouter>();

// Register validators //TODO: Move to extension method
builder.Services.AddTransient<IValidator<EndUserRequestModel>, EndUserRequestModelValidator>();
builder.Services.AddTransient<IValidator<ContentCreatorRequestModel>, ContentCreatorRequestModelValidator>();
builder.Services.AddTransient<IValidator<SignInRequestModel>, SignInRequestModelValidator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
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
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIGateway API V1");
        //c.RoutePrefix = string.Empty; // Make Swagger UI the root page
    });
}

app.UseMiddleware<JwtConfigMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


