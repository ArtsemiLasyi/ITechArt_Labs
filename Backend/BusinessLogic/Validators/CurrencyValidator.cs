using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class CurrencyValidator : AbstractValidator<CurrencyModel>
    {
        public CurrencyValidator()
        {
            RuleFor(currency => currency.Name)
                .NotNull()
                .MaximumLength(3)
                .WithMessage("Currency name must not be greater than 3 letters");
            RuleFor(currency => currency.Id)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("No currency selected");
        }
    }
}
