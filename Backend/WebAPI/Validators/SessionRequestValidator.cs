using BusinessLogic.Models;
using BusinessLogic.Validators;
using FluentValidation;
using Mapster;
using System.Collections.Generic;
using WebAPI.Requests;

namespace WebAPI.Validators
{
    public class SessionRequestValidator : AbstractValidator<SessionRequest>
    {
        public SessionRequestValidator(SessionValidator validator, SeatTypePriceValidator seatTypePriceValidator)
        {
            RuleFor(request => request.Adapt<SessionModel>()).SetValidator(validator);
            RuleForEach(request => request.SeatTypePrices.Adapt<IReadOnlyCollection<SeatTypePriceModel>>())
                .SetValidator(seatTypePriceValidator)
                .OverridePropertyName("SeatTypePrices");
        }
    }
}
