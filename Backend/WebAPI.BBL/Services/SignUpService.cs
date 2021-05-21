using BusinessLogic.Models;
using BusinessLogic.Utils;
using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class SignUpService
    {
        private readonly UserRepository _userRepository;

        public SignUpService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> SignUp(SignUpModel model)
        {
            UserEntity? userEntity = _userRepository.GetByEmail(model.Email);
            if (userEntity != null)
            {
                return false;
            }

            byte[] salt = AuthentificationUtils.GenerateSalt();
            byte[] hash = AuthentificationUtils.ComputeHash(model.Password, salt);

            userEntity = new UserEntity
            {
                Email = model.Email,
                PasswordHash = hash,
                Salt = salt,
                RoleId = (int)UserRoleModel.CommonUser
            };
            await _userRepository.Create(userEntity);
            return true;
        }
    }
}
