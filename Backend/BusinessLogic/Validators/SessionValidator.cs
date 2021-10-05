using BusinessLogic.Models;
using FluentValidation;
using System;

namespace BusinessLogic.Validators
{
    public class SessionValidator : AbstractValidator<SessionModel>
    {
        public SessionValidator()
        {
            RuleFor(model => model.FilmId)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("No film selected");
            RuleFor(model => model.HallId)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("No hall selected");
            RuleFor(model => model.StartDateTime)
                .NotNull()
                .GreaterThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Session start can not be in the past");
        }
    }
}
