using APIGatewayControllers.DTO.Models.Requests;
using APIGatewayControllers.Models.Requests;
using APIGatewayControllers.Validators;
using FluentValidation;

namespace APIGatewayMain.ServiceCollectionExtensions
{

    public static class ValidationExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services) 
        {
            services.AddTransient<IValidator<EndUserRequestModel>, EndUserRequestModelValidator>();
            services.AddTransient<IValidator<ContentCreatorRequestModel>, ContentCreatorRequestModelValidator>();
            services.AddTransient<IValidator<SignInRequestModel>, SignInRequestModelValidator>();
            return services;
        }
    }
    
}
