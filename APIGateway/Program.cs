using APIGatewayControllers.Middlewares;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using APIGatewayControllers;
using APIGatewayMain.ServiceCollectionExtensions;
using APIGatewayMain.ServiceCollectionExtensions.ComponentsExtensions;
using AspNetCoreRateLimit;

//TODO: Duration handling 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCommonConfiguration(builder.Configuration);
builder.Services.AddRateLimiting();
builder.Services.AddAuthorizationServiceAPI();
builder.Services.AddJWTConfiguration();
builder.Services.AddContentMetadataMock();
//builder.Services.AddStreamGatewayMock();
builder.Services.AddStreamGatewayAPI();
builder.Services.AddLicenseProxyApi();
builder.Services.AddEntityComponent();
builder.Services.AddValidators();

builder.Services.AddControllers().PartManager.ApplicationParts
    .Add(new AssemblyPart(typeof(ControllersAssemblyMarker).Assembly));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIGateway API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter JWT token with format: {Bearer <token>}",
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

app.UseIpRateLimiting();

app.MapControllers();

app.Run();


