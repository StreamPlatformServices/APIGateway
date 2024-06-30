﻿using APIGatewayControllers.DTO.Models.Requests;
using FluentValidation;

namespace APIGatewayControllers.Validators
{
    public class EndUserRequestModelValidator : AbstractValidator<EndUserRequestModel>
    {
        public EndUserRequestModelValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required.");
        }
    }
}