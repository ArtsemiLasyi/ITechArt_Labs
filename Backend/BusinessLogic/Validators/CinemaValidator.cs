using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class CinemaValidator : AbstractValidator<CinemaModel>
    {
        public CinemaValidator()
        {
            RuleFor(model => model.CityName).NotNull().MaximumLength(50);
            RuleFor(model => model.Name).NotNull().MaximumLength(50);
            RuleFor(model => model.Description).NotNull();
        }
    }
}
