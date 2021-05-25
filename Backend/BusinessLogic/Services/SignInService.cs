using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using Mapster;

namespace BusinessLogic.Services
{
    public class SignInService
    {
        private readonly UserRepository _userRepository;
        private readonly PasswordService _passwordService;

        public SignInService(
            UserRepository userRepository,
            PasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        public UserModel? SignIn(SignInModel model)
        {
            UserEntity? userEntity = _userRepository.GetByEmail(model.Email);
            if (userEntity == null)
            {
                return null;
            }

            bool equal = _passwordService.CheckPassword(userEntity.Id, model.Password);
            if (equal)
            {
                UserModel userModel = userEntity.Adapt<UserModel>();
                return userModel;
            }
            return null;
        }
    }
}
