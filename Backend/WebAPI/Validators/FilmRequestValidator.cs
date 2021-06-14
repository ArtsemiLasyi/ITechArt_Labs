using BusinessLogic.Models;
using BusinessLogic.Validators;
using FluentValidation;
using Mapster;
using WebAPI.Requests;

namespace WebAPI.Validators
{
    public class FilmRequestValidator : AbstractValidator<FilmRequest>
    {
        public FilmRequestValidator(FilmValidator validator)
        {
            RuleFor(request => request.Adapt<FilmModel>()).SetValidator(validator);
        }
    }
}
