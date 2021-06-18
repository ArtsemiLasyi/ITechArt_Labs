using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;

namespace WebAPI.Validators
{
    public class FormFileValidator : AbstractValidator<IFormFile>
    {
        public FormFileValidator()
        {
            RuleFor(file => file).Must(
                file =>
                {
                    return file.ContentType == MediaTypeNames.Image.Jpeg || file.ContentType == "image/png";
                }
            );
        }
    }
}
