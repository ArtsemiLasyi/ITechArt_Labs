using BusinessLogic.Models;
using BusinessLogic.Validators;
using FluentValidation;
using Mapster;
using WebAPI.Requests;

namespace WebAPI.Validators
{
    public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
    {
        public SignUpRequestValidator(SignUpValidator validator)
        {
            RuleFor(request => request.Adapt<SignUpModel>()).SetValidator(validator);
        }
    }
}
