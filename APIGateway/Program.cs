using APIGatewayControllers.Middlewares;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using APIGatewayControllers;
using APIGatewayMain.ServiceCollectionExtensions;
using APIGatewayMain.ServiceCollectionExtensions.ComponentsExtensions;
using AspNetCoreRateLimit;
using APIGatewayCoreUtilities.CommonConfiguration.ConfigurationModels;

//TODO: List current config in console log
//TODO: add path to config
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCommonConfiguration(builder.Configuration);

var kestrelSettings = builder.Configuration.GetSection("KestrelSettings").Get<KestrelSettings>() ?? throw new Exception("Fatal error: Please provide kestrel configuration");
builder.AddKestrelSettings(kestrelSettings);

builder.Services.AddRateLimiting();
builder.Services.AddAuthorizationServiceAPI();
builder.Services.AddJWTConfiguration();
builder.Services.AddContentMetadataMock();
builder.Services.AddStreamGatewayAPI();
builder.Services.AddLicenseServiceClient();
builder.Services.AddEntityComponent();
builder.Services.AddValidators();

var corsPolicyName = "CustomCorsPolicy";
var corsSettings = builder.Configuration.GetSection("CorsSettings").Get<CorsSettings>() ?? throw new Exception("Fatal error: Please provide CorsSettings configuration");
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName,
        policy =>
        {
            policy.WithOrigins(corsSettings.AllowedHosts)
                .WithHeaders(corsSettings.AllowedHeaders)
                .WithMethods(corsSettings.AllowedMethods);
        });
});

builder.Services.AddControllers().PartManager.ApplicationParts
    .Add(new AssemblyPart(typeof(ControllersAssemblyMarker).Assembly));


var useSwagger = builder.Configuration.GetSection("UseSwagger").Get<bool>();

if (useSwagger)
{
    builder.Services.AddSwaggerSettings();
}

var app = builder.Build();

if (useSwagger)
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIGateway API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors(corsPolicyName);
app.UseMiddleware<JwtConfigMiddleware>();

if (kestrelSettings.UseTls)
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseIpRateLimiting();

app.MapControllers();

app.Run();


