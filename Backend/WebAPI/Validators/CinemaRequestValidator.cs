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
        public CinemaRequestValidator(CityValidator cityValidator, CinemaValidator cinemaValidator)
        {
            RuleFor(request => request.Adapt<CinemaModel>()).SetValidator(cinemaValidator);
            RuleFor(
                request => 
                    new CityModel() 
                    {
                        Name = request.CityName 
                    }
                )
                .SetValidator(cityValidator);
        }
    }
}
