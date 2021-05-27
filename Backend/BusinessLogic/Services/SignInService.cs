using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using Mapster;

namespace BusinessLogic.Services
{
    public class SignInService
    {
        private readonly UserService _userService;
        private readonly PasswordService _passwordService;

        public SignInService(
            UserService userService,
            PasswordService passwordService)
        {
            _userService = userService;
            _passwordService = passwordService;
        }

        public UserModel? SignIn(SignInModel model)
        {
            UserModel? user = _userService.GetBy(model.Email);
            if (user == null)
            {
                return null;
            }

            bool equal = _passwordService.CheckPasswordMatch(user.Id, model.Password);
            if (equal)
            {
                return user;
            }
            return null;
        }
    }
}
