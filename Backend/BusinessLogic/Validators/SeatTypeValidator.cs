using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class SeatTypeValidator : AbstractValidator<SeatTypeModel>
    {
        public SeatTypeValidator()
        {
            RuleFor(seatType => seatType.Name).NotNull().MaximumLength(50);
        }
    }
}
