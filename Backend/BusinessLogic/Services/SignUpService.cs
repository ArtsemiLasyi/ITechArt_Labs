using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class SignUpService
    {
        private readonly UserService _userService;
        private readonly PasswordService _passwordService;

        public SignUpService(
            UserService userService,
            PasswordService passwordService)
        {
            _userService = userService;
            _passwordService = passwordService;
        }

        public async Task<bool> SignUpAsync(SignUpModel model)
        {
            UserModel? user = _userService.Get(model.Email);
            if (user != null)
            {
                return false;
            }

            user = new UserModel
            {
                Email = model.Email
            };
            user = await _userService.CreateAsync(user);
            await _passwordService.CreatePasswordAsync(user.Id, model.Password);
            
            return true;
        }
    }
}
