using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class SeatValidator : AbstractValidator<SeatModel>
    {
        public SeatValidator()
        {
            RuleFor(seat => seat.Place)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Row place be a number greater then 0");
            RuleFor(seat => seat.Row)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Row must be a number greater then 0");
            RuleFor(seat => seat.SeatTypeId)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("No seat type selected");
            RuleFor(seat => seat.HallId)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("No hall selected");
        }
    }
}
