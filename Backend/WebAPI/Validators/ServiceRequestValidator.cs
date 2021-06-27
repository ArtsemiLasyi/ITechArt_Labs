using BusinessLogic.Models;
using BusinessLogic.Validators;
using FluentValidation;
using Mapster;
using WebAPI.Requests;

namespace WebAPI.Validators
{
    public class ServiceRequestValidator : AbstractValidator<ServiceRequest>
    {
        public ServiceRequestValidator(ServiceValidator validator)
        {
            RuleFor(request => request.Adapt<ServiceModel>()).SetValidator(validator);
        }
    }
}
