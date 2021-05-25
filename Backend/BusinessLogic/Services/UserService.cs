using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            UserEntity? user = _userRepository.GetById(id);
            if (user == null)
            {
                return null;
            }
            return user.Adapt<UserModel>(); 
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> EditUser(UserModel model)
        {
            UserEntity userEntity = model.Adapt<UserEntity>();
            await _userRepository.UpdateAsync(userEntity);
            return true;
        }
    }
}
