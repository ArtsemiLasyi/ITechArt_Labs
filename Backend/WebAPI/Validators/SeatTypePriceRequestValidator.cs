using BusinessLogic.Models;
using BusinessLogic.Validators;
using FluentValidation;
using Mapster;
using WebAPI.Requests;

namespace WebAPI.Validators
{
    public class SeatTypePriceRequestValidator : AbstractValidator<SeatTypePriceRequest>
    {
        public SeatTypePriceRequestValidator(SeatTypePriceValidator seatTypePriceValidator)
        {
            RuleFor(request => request.Adapt<SeatTypePriceModel>()).SetValidator(seatTypePriceValidator);
        }
    }
}
