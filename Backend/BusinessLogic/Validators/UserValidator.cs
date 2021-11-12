using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            RuleFor(model => model.Email)
                .EmailAddress()
                .WithMessage("It is not valid email address");
        }
    }
}
