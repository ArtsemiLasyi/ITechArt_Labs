using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class SeatTypeValidator : AbstractValidator<SeatTypeModel>
    {
        public SeatTypeValidator()
        {
            RuleFor(seatType => seatType.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Seat type name must not be greater than 50 letters");
            RuleFor(seatType => seatType.ColorRgb)
                .NotNull()
                .NotEmpty()
                .Length(7)
                .WithMessage("Invalid color selected");
        }
    }
}
