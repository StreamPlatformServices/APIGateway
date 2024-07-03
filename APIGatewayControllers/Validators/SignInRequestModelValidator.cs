using APIGatewayControllers.Models.Requests;
using FluentValidation;

namespace APIGatewayControllers.Validators
{
    public class SignInRequestModelValidator : AbstractValidator<SignInRequestModel>
    {
        public SignInRequestModelValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
