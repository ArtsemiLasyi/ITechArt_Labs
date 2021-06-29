using BusinessLogic.Models;
using BusinessLogic.Validators;
using FluentValidation;
using Mapster;
using WebAPI.Requests;

namespace WebAPI.Validators
{
    public class SeatTypeRequestValidator : AbstractValidator<SeatTypeRequest>
    {
        public SeatTypeRequestValidator(SeatTypeValidator validator)
        {
            RuleFor(request => request.Adapt<SeatTypeModel>()).SetValidator(validator);
        }
    }
}
