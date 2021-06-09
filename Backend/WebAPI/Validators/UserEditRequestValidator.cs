using BusinessLogic.Models;
using BusinessLogic.Validators;
using FluentValidation;
using Mapster;
using WebAPI.Requests;

namespace WebAPI.Validators
{
    public class UserEditRequestValidator : AbstractValidator<UserEditRequest>
    {
        public UserEditRequestValidator(UserValidator userValidator, PasswordValidator passwordValidator)
        {
            RuleFor(request => request.Adapt<UserModel>()).SetValidator(userValidator);
            RuleFor(request => request.Password).SetValidator(passwordValidator);
        }
    }
}
