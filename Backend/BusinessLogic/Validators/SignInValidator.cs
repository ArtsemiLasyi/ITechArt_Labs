using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class SignInValidator : AbstractValidator<SignInModel>
    {
        public SignInValidator()
        {
            RuleFor(model => model.Email).SetValidator(new EmailValidator());
            RuleFor(model => model.Password).SetValidator(new PasswordValidator());
        }
    }
}
