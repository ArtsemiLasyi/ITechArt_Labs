using BusinessLogic.Models;
using BusinessLogic.Validators;
using FluentValidation;
using Mapster;
using System.Collections.Generic;
using WebAPI.Requests;

namespace WebAPI.Validators
{
    public class HallRequestValidator : AbstractValidator<HallRequest>
    {
        public HallRequestValidator(HallValidator hallValidator)
        {
            RuleFor(request => request.Adapt<HallModel>()).SetValidator(hallValidator);
        }
    }
}
