using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class HallValidator : AbstractValidator<HallModel>
    {
        public HallValidator()
        {
            RuleFor(model => model.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Hall name must not be greater than 50 letters");
            RuleFor(model => model.CinemaId)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("No cinema selected");
        }
    }
}
