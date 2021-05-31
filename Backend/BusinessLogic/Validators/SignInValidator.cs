using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class SignInValidator : AbstractValidator<SignInModel>
    {
        public SignInValidator()
        {
            RuleFor(model => model.Email);
            RuleFor(model => model.Password).SetValidator(new PasswordValidator());
        }
    }
}
