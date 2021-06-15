using BusinessLogic.Models;
using BusinessLogic.Validators;
using FluentValidation;
using Mapster;
using WebAPI.Requests;

namespace WebAPI.Validators
{
    public class FilmCreateRequestValidator : AbstractValidator<FilmCreateRequest>
    {
        public FilmCreateRequestValidator(FilmValidator validator)
        {
            RuleFor(request => request.Adapt<FilmModel>()).SetValidator(validator);
            RuleFor(request => request.Poster).NotNull();
        }
    }
}
