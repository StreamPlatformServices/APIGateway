using APIGatewayControllers.Models.Requests.User;
using FluentValidation;

namespace APIGatewayControllers.Validators
{
    public class UpdateEndUserRequestModelValidator : AbstractValidator<UpdateEndUserRequestModel>
    {
        public UpdateEndUserRequestModelValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required.");
        }
    }
}
