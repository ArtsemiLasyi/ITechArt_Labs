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
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Film name must not be greater than 50 letters");
            RuleFor(film => film.ReleaseYear)
                .GreaterThanOrEqualTo(FIRST_FILM_RELEASE_YEAR)
                .WithMessage($"Release year of film must be greater than or equal to {FIRST_FILM_RELEASE_YEAR}");
            RuleFor(film => film.Duration.TotalMinutes)
                .GreaterThan(0)
                .WithMessage("Film duration must be greater than 0");
            RuleFor(film => film.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("Film description can not be empty");
        }
    }
}
