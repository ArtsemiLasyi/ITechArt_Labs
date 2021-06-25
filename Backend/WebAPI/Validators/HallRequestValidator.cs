using BusinessLogic.Models;
using BusinessLogic.Validators;
using FluentValidation;
using Mapster;
using System.Collections.Generic;
using WebAPI.Requests;

namespace WebAPI.Validators
{
    public class HallRequestValidator : AbstractValidator<HallRequest>
    {
        public HallRequestValidator(SeatValidator seatValidator, HallValidator hallValidator)
        {
            RuleFor(request => request.Adapt<HallModel>()).SetValidator(hallValidator);
            RuleForEach(request => request.Seats.Adapt<IReadOnlyCollection<SeatModel>>())
                .SetValidator(seatValidator)
                .OverridePropertyName("Seats");
        }
    }
}
