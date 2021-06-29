using BusinessLogic.Models;
using BusinessLogic.Validators;
using FluentValidation;
using Mapster;
using System.Collections.Generic;
using WebAPI.Requests;

namespace WebAPI.Validators
{
    public class SeatsRequestValidator : AbstractValidator<SeatsRequest>
    {
        public SeatsRequestValidator(SeatValidator seatValidator)
        {
            RuleForEach(request => request.Seats.Adapt<IReadOnlyCollection<SeatModel>>())
                .SetValidator(seatValidator)
                .OverridePropertyName("Seats");
        }
    }
}
