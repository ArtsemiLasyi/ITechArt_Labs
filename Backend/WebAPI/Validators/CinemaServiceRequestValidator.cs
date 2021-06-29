using BusinessLogic.Models;
using BusinessLogic.Validators;
using FluentValidation;
using Mapster;
using WebAPI.Requests;

namespace WebAPI.Validators
{
    public class CinemaServiceRequestValidator : AbstractValidator<CinemaServiceRequest>
    {
        public CinemaServiceRequestValidator(CinemaServiceValidator cinemaServiceValidator)
        {
            RuleFor(request => request.Adapt<CinemaServiceModel>()).SetValidator(cinemaServiceValidator);
        }
    }
}
