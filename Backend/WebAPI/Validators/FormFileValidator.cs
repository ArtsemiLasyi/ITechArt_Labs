using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Validators
{
    public class FormFileValidator : AbstractValidator<IFormFile>
    {
        private const string IMAGE_JPEG_MIME_TYPE = "image/jpeg";
        private const string IMAGE_PNG_MIME_TYPE = "image/png";

        public FormFileValidator()
        {
            RuleFor(file => file).Must(
                file =>
                {
                    return file.ContentType == IMAGE_JPEG_MIME_TYPE || file.ContentType == IMAGE_PNG_MIME_TYPE;
                }
            );
        }
    }
}
