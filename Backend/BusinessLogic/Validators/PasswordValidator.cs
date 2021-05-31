using FluentValidation;

namespace BusinessLogic.Validators
{
    public class PasswordValidator : AbstractValidator<string?>
    {
        public PasswordValidator()
        {
            RuleFor(password => password).MinimumLength(6);
        }
    }
}