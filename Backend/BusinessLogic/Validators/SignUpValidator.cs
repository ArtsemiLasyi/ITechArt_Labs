using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class SignUpValidator : AbstractValidator<SignUpModel>
    {
        public SignUpValidator()
        {
            RuleFor(model => model.Email);
            RuleFor(model => model.Password).SetValidator(new PasswordValidator());
        }
    }
}
