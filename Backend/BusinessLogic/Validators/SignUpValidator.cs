using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class SignUpValidator : AbstractValidator<SignUpModel>
    {
        public SignUpValidator()
        {
            RuleFor(model => model.Email).SetValidator(new EmailValidator());
            RuleFor(model => model.Password).SetValidator(new PasswordValidator());
        }
    }
}
