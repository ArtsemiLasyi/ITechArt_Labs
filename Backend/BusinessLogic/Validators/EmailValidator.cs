using FluentValidation;

namespace BusinessLogic.Validators
{
	public class EmailValidator : AbstractValidator<string>
	{
		public EmailValidator()
		{
			RuleFor(email => email).EmailAddress();
		}
	}
}
