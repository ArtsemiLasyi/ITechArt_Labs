using FluentValidation;
using WebAPI.Requests;

namespace WebAPI.Validators
{
    public class PageRequestValidator : AbstractValidator<PageRequest>
    {
        public PageRequestValidator()
        {
            RuleFor(request => request.PageNumber).GreaterThan(0);
            RuleFor(request => request.PageSize).GreaterThan(0);
        }
    }
}
