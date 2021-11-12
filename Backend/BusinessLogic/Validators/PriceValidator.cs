using BusinessLogic.Models;
using FluentValidation;
using Mapster;

namespace BusinessLogic.Validators
{
    public class PriceValidator : AbstractValidator<PriceModel>
    {
        public PriceValidator(CurrencyValidator validator)
        {
            RuleFor(model => model.Currency).SetValidator(validator);
            RuleFor(model => model.Value)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price can not be less than 0");
        }
    }
}
