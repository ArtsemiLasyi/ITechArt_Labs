using BusinessLogic.Models;
using FluentValidation;
using Mapster;
using System.Collections.Generic;

namespace BusinessLogic.Validators
{
    public class OrderValidator : AbstractValidator<OrderModel>
    {
        public OrderValidator(PriceValidator priceValidator, SeatValidator seatValidator)
        {
            RuleFor(model => model.Price).SetValidator(priceValidator);
            RuleForEach(request => request.Seats.Adapt<IReadOnlyCollection<SeatModel>>())
                .SetValidator(seatValidator)
                .OverridePropertyName("Seats");
        }
    }
}
