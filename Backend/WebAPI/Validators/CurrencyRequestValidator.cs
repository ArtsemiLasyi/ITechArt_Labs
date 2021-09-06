using BusinessLogic.Models;
using BusinessLogic.Validators;
using FluentValidation;
using Mapster;
using WebAPI.Requests;

namespace WebAPI.Validators
{
    public class CurrencyRequestValidator : AbstractValidator<CurrencyRequest>
    {
        public CurrencyRequestValidator(CurrencyValidator validator)
        {
            RuleFor(request => request.Adapt<CurrencyModel>()).SetValidator(validator);
        }
    }
}
