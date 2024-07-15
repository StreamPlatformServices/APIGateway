using APIGatewayControllers.Models.Requests.Content;
using FluentValidation;

namespace APIGatewayControllers.Validators
{
    public class UploadContentRequestModelValidator : AbstractValidator<UploadContentRequestModel> 
    {
        public UploadContentRequestModelValidator()
        {
            RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Email is required.")
            .MinimumLength(6).WithMessage("Title must be at least 6 characters long.")
            .MaximumLength(128).WithMessage("Title can't exceed 128 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(x => x.LicenseRules)
                .NotEmpty().WithMessage("At least one license rule is needed.");
        }
    }
}
