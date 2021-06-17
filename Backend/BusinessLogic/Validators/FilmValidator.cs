using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class FilmValidator : AbstractValidator<FilmModel>
    {
        private const int FIRST_FILM_RELEASE_YEAR = 1895;

        public FilmValidator()
        {
            RuleFor(film => film.Name)
                .NotNull()
                .MaximumLength(50);
            RuleFor(film => film.ReleaseYear).GreaterThanOrEqualTo(FIRST_FILM_RELEASE_YEAR);
            RuleFor(film => film.Duration.TotalMinutes).GreaterThan(0);
            RuleFor(film => film.Description).NotNull();
        }
    }
}
