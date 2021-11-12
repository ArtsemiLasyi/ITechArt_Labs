using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class ServiceValidator : AbstractValidator<ServiceModel>
    {
        public ServiceValidator()
        {
            RuleFor(model => model.Name)
                .NotNull()
                .MaximumLength(50)
                .WithMessage("Service name must not be greater then 50 letters");
        }
    }
}
