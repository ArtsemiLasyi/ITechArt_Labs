using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class SignInValidator : AbstractValidator<SignInModel>
    {
        public SignInValidator()
        {
            RuleFor(model => model.Email).EmailAddress().WithMessage("It is not valid email address");
            RuleFor(model => model.Password).SetValidator(new PasswordValidator());
        }
    }
}
