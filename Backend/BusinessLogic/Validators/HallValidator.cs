using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class HallValidator : AbstractValidator<HallModel>
    {
        public HallValidator()
        {
            RuleFor(model => model.Name).NotNull().MaximumLength(50);
        }
    }
}
