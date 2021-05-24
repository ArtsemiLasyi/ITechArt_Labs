using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Utils;
using BusinessLogic.Models;
using DataAccess.Entities;
using Mapster;
using DataAccess.Repositories;

namespace BusinessLogic.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserModel? GetUser(int id)
        {
            UserEntity user = _userRepository.FindFirst(user => id == user.Id);
            if (user == null)
            {
                return null;
            }
            return new UserModel { Id = user.Id, Email = user.Email };
        }

        public async Task<bool> DeleteUser(int id)
        {
            UserModel? user = GetUser(id);
            if (user == null)
            {
                return false;
            }
            await _userRepository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> EditUser(UserModel model, string password)
        {
            byte[] salt = AuthentificationUtils.GenerateSalt();
            byte[] hash = AuthentificationUtils.ComputeHash(password, salt);

            UserEntity userEntity = new UserEntity
            {
                Email = model.Email,
                PasswordHash = hash,
                Salt = salt
            };
            await _userRepository.UpdateAsync(userEntity);
            return true;
        }
    }
}
