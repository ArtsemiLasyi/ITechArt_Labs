﻿using BusinessLogic.Models;
using FluentValidation;
using Mapster;
using System.Collections.Generic;

namespace BusinessLogic.Validators
{
    public class OrderValidator : AbstractValidator<OrderModel>
    {
        public OrderValidator(PriceValidator priceValidator)
        {
            RuleFor(model => model.Price).SetValidator(priceValidator);
            RuleFor(model => model.SessionSeats)
                .NotNull()
                .WithMessage("No seats were sent");
            RuleFor(model => model.CinemaServices)
                .NotNull()
                .WithMessage("No services were sent");
        }
    }
}
