using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Validators
{
    public class IFormFileValidator : AbstractValidator<IFormFile>
    {
        public IFormFileValidator()
        {
            RuleFor(file => file).NotNull();
        }
    }
}
