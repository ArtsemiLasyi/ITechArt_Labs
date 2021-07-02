using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class PriceValidator : AbstractValidator<PriceModel>
    {
        public PriceValidator()
        {
            RuleFor(model => model.Currency).NotNull().MaximumLength(3);
            RuleFor(model => model.Value).NotNull().GreaterThanOrEqualTo(0);
        }
    }
}
