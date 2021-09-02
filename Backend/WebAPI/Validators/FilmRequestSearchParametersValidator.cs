using FluentValidation;
using WebAPI.Parameters;

namespace WebAPI.Validators
{
    public class FilmRequestSearchParametersValidator : AbstractValidator<FilmRequestSearchParameters>
    {
        public FilmRequestSearchParametersValidator()
        {
            RuleFor(request => request.PageNumber).GreaterThan(0);
            RuleFor(request => request.PageSize).GreaterThan(0);
        }
    }
}
