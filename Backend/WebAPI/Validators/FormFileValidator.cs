using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;

namespace WebAPI.Validators
{
    public class FormFileValidator : AbstractValidator<IFormFile>
    {
        private const string IMAGE_PNG_MIME_TYPE = "image/png";

        public FormFileValidator()
        {
            RuleFor(file => file).Must(
                file =>
                {
                    return file.ContentType == MediaTypeNames.Image.Jpeg || file.ContentType == IMAGE_PNG_MIME_TYPE;
                }
            );
        }
    }
}
