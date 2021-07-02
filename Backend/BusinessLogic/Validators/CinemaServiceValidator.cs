using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class CinemaServiceValidator : AbstractValidator<CinemaServiceModel>
    {
        public CinemaServiceValidator(PriceValidator validator)
        {
            RuleFor(model => model.Price).SetValidator(validator);
        }
    }
}
