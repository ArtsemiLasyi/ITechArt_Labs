using BusinessLogic.Models;
using BusinessLogic.Validators;
using FluentValidation;
using Mapster;
using WebAPI.Requests;

namespace WebAPI.Validators
{
    public class FilmEditRequestValidator : AbstractValidator<FilmCreateRequest>
    {
        public FilmEditRequestValidator(FilmValidator validator)
        {
            RuleFor(request => request.Adapt<FilmModel>()).SetValidator(validator);
        }
    }
}
