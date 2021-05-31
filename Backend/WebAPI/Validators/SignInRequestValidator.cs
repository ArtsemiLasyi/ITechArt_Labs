using BusinessLogic.Models;
using BusinessLogic.Validators;
using FluentValidation;
using Mapster;
using WebAPI.Requests;

namespace WebAPI.Validators
{
    public class SignInRequestValidator : AbstractValidator<SignInRequest>
    {
        public SignInRequestValidator(SignInValidator validator)
        {
            RuleFor(request => request.Adapt<SignInModel>()).SetValidator(validator);
        }
    }
}
