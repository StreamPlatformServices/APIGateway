using APIGatewayControllers.Models.Requests.User;
using FluentValidation;

namespace APIGatewayControllers.Validators.User
{
    public class UpdateContentCreatorRequestModelValidator : AbstractValidator<UpdateContentCreatorRequestModel> //TODO: decide about password format req
    {
        public UpdateContentCreatorRequestModelValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required.");

            RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\d{9}$").WithMessage("Phone number must be a 9-digit number.");

            RuleFor(x => x.NIP)
                .NotEmpty().WithMessage("NIP is required.")
                .Matches(@"^\d{10}$").WithMessage("NIP must be a 10-digit number.");
        }
    }
}
