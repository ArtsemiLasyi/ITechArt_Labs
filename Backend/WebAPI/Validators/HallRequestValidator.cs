using BusinessLogic.Models;
using BusinessLogic.Validators;
using FluentValidation;
using Mapster;
using WebAPI.Requests;

namespace WebAPI.Validators
{
    public class HallRequestValidator : AbstractValidator<HallRequest>
    {
        public HallRequestValidator(HallValidator validator)
        {
            RuleFor(request => request.Adapt<HallModel>()).SetValidator(validator);
        }
    }
}
