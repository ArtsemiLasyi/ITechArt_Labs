using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class CityValidator : AbstractValidator<CityModel>
    {
        public CityValidator()
        {
            RuleFor(city => city.Name).NotNull().MaximumLength(50);
        }
    }
}
