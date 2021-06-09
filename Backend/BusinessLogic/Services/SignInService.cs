using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using Mapster;
using System.Threading.Tasks;

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

        public async Task<UserModel?> SignInAsync(SignInModel model)
        {
            UserModel? user = await _userService.GetByAsync(model.Email);
            if (user == null)
            {
                return null;
            }

            bool equal = await _passwordService.MatchForUserAsync(user.Id, model.Password);
            if (equal)
            {
                return user;
            }
            return null;
        }
    }
}
