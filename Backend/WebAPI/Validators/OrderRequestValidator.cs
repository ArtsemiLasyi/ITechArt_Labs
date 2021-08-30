using BusinessLogic.Models;
using BusinessLogic.Validators;
using FluentValidation;
using Mapster;
using WebAPI.Requests;

namespace WebAPI.Validators
{
    public class OrderRequestValidator : AbstractValidator<OrderRequest>
    {
        public OrderRequestValidator(OrderValidator orderValidator, SeatValidator seatValidator)
        {
            RuleFor(request => request.Adapt<OrderModel>()).SetValidator(orderValidator);
        }
    }
}
