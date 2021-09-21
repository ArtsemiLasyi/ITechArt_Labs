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
                .MaximumLength(50);
            RuleFor(model => model.CinemaId)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
