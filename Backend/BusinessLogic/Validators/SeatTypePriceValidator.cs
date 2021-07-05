using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class SeatTypePriceValidator : AbstractValidator<SeatTypePriceModel>
    {
        public SeatTypePriceValidator(PriceValidator validator)
        {
            RuleFor(model => model.Price).SetValidator(validator);
        }
    }
}
