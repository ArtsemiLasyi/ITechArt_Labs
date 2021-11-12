using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class CinemaValidator : AbstractValidator<CinemaModel>
    {
        public CinemaValidator()
        {
            RuleFor(model => model.CityName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("City name must not be greater than 50 letters");
            RuleFor(model => model.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Cinema name must not be greater than 50 letters");
            RuleFor(model => model.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("Cinema description must not be empty");
        }
    }
}
