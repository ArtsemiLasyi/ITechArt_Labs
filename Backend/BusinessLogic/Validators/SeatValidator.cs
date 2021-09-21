using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class SeatValidator : AbstractValidator<SeatModel>
    {
        public SeatValidator()
        {
            RuleFor(seat => seat.Place).NotNull().GreaterThan(0);
            RuleFor(seat => seat.Row).NotNull().GreaterThan(0);
            RuleFor(seat => seat.SeatTypeId).NotNull().GreaterThan(0);
            RuleFor(seat => seat.HallId).NotNull().GreaterThan(0);
        }
    }
}
