using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class SignUpService
    {
        private readonly UserRepository _userRepository;
        private readonly PasswordService _passwordService;

        public SignUpService(
            UserRepository userRepository,
            PasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        public async Task<bool> SignUpAsync(SignUpModel model)
        {
            UserEntity? userEntity = _userRepository.GetByEmail(model.Email);
            if (userEntity != null)
            {
                return false;
            }

            userEntity = new UserEntity
            {
                Email = model.Email,
                RoleId = (int)UserRole.CommonUser
            };
            await _userRepository.CreateAsync(userEntity);
            await _passwordService.CreatePasswordAsync(userEntity.Id, model.Password);
            
            return true;
        }
    }
}
