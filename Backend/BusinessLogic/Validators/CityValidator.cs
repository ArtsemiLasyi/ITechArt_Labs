﻿using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class CityValidator : AbstractValidator<CityModel>
    {
        public CityValidator()
        {
            RuleFor(city => city.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("City name must not be greater than 50 letters");
        }
    }
}
