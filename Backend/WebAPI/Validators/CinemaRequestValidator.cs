using BusinessLogic.Models;
using BusinessLogic.Validators;
using FluentValidation;
using Mapster;
using WebAPI.Requests;

namespace WebAPI.Validators
{
    public class CinemaRequestValidator : AbstractValidator<CinemaRequest>
    {
        public CinemaRequestValidator(CinemaValidator validator)
        {
            RuleFor(request => request.Adapt<CinemaModel>()).SetValidator(validator);
        }
    }
}
