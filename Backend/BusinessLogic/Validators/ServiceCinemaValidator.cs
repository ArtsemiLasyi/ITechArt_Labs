using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class ServiceCinemaValidator : AbstractValidator<ServiceCinemaModel>
    {
        public ServiceCinemaValidator()
        {
            RuleFor(model => model.PriceInDollars).GreaterThanOrEqualTo(0);
        }
    }
}
