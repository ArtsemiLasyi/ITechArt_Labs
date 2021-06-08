using FluentValidation;

namespace BusinessLogic.Validators
{
    public class PasswordValidator : AbstractValidator<string?>
    {
        public PasswordValidator()
        {
            RuleFor(password => password)
                .Custom(
                    (password, context) =>
                    {
                        if(password != null)
                        {
                            if(password.Length < 6)
                            {
                                context.AddFailure("Password must not be less than 6 symbols");
                            }
                        }
                    }
                );
        }
    }
}
