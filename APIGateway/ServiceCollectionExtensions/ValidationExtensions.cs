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
            services.AddTransient<IValidator<AddEndUserRequestModel>, AddEndUserRequestModelValidator>();
            services.AddTransient<IValidator<AddContentCreatorRequestModel>, AddContentCreatorRequestModelValidator>();
            services.AddTransient<IValidator<SignInRequestModel>, SignInRequestModelValidator>();
            return services;
        }
    }
    
}
