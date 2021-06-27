using BusinessLogic.Models;
using BusinessLogic.Validators;
using FluentValidation;
using Mapster;
using System.Collections.Generic;
using WebAPI.Requests;

namespace WebAPI.Validators
{
    public class CinemaRequestValidator : AbstractValidator<CinemaRequest>
    {
        public CinemaRequestValidator(ServiceCinemaValidator serviceCinemaValidator, CinemaValidator cinemaValidator)
        {
            RuleFor(request => request.Adapt<CinemaModel>()).SetValidator(cinemaValidator);
            RuleForEach(request => request.ServiceCinemas.Adapt<IReadOnlyCollection<ServiceCinemaModel>>())
                .SetValidator(serviceCinemaValidator)
                .OverridePropertyName("ServiceCinemas");
        }
    }
}
