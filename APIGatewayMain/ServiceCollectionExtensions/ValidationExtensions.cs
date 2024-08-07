﻿using APIGatewayControllers.Models.Requests.Content;
using APIGatewayControllers.Models.Requests.User;
using APIGatewayControllers.Validators;
using APIGatewayControllers.Validators.User;
using FluentValidation;

namespace APIGatewayMain.ServiceCollectionExtensions
{

    public static class ValidationExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services) 
        {
            services.AddTransient<IValidator<AddEndUserRequestModel>, AddEndUserRequestModelValidator>();
            services.AddTransient<IValidator<AddContentCreatorRequestModel>, AddContentCreatorRequestModelValidator>();
            services.AddTransient<IValidator<UpdateEndUserRequestModel>, UpdateEndUserRequestModelValidator>();
            services.AddTransient<IValidator<UpdateContentCreatorRequestModel>, UpdateContentCreatorRequestModelValidator>();
            services.AddTransient<IValidator<SignInRequestModel>, SignInRequestModelValidator>();
            services.AddTransient<IValidator<ChangePasswordRequestModel>, ChangePasswordRequestModelValidator>();

            services.AddTransient<IValidator<UploadContentRequestModel>, UploadContentRequestModelValidator>();
            return services;
        }
    }
    
}
