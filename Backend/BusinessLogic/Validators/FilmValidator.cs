using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class FilmValidator : AbstractValidator<FilmModel>
    {
        public FilmValidator()
        {
            RuleFor(film => film.Name).MaximumLength(50);
            RuleFor(film => film.ReleaseYear).GreaterThanOrEqualTo(0);
            RuleFor(film => film.Duration.TotalMinutes).GreaterThanOrEqualTo(0);
        }
    }
}
