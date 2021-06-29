using FluentValidation;
using WebAPI.Requests;

namespace WebAPI.Validators
{
    public class SeatsRequestValidator : AbstractValidator<SeatsRequest>
    {
        public SeatsRequestValidator()
        {

        }
    }
}
