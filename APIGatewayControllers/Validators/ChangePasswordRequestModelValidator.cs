using APIGatewayControllers.Models.Requests;
using FluentValidation;

namespace APIGatewayControllers.Validators
{
    public class ChangePasswordRequestModelValidator : AbstractValidator<ChangePasswordRequestModel>
    {
        public ChangePasswordRequestModelValidator()
        {
            RuleFor(x => x.OldPassword)
            .NotEmpty().WithMessage("Old password is required");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}
