using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class CurrencyValidator : AbstractValidator<CurrencyModel>
    {
        public CurrencyValidator()
        {
            RuleFor(currency => currency.Name).NotNull().MaximumLength(3);
        }
    }
}
