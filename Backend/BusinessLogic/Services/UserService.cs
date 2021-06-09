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

        public async Task<UserModel> CreateAsync(UserModel user)
        {
            UserEntity? userEntity = new();
            userEntity.RoleId = (int)UserRole.User;
            await _userRepository.CreateAsync(userEntity);
            return userEntity.Adapt<UserModel>();
        }

        public async Task<UserModel?> GetByAsync(string email)
        {
            UserEntity? user = await _userRepository.GetByAsync(email);
            if (user == null)
            {
                return null;
            }
            return user.Adapt<UserModel>();
        }

        public async Task<UserModel?> GetByAsync(int id)
        {
            UserEntity? user = await _userRepository.GetByAsync(id);
            if (user == null)
            {
                return null;
            }
            return user.Adapt<UserModel>(); 
        }

        public Task<bool> DeleteByAsync(int id)
        {
           return _userRepository.DeleteByAsync(id);
        }

        public Task EditAsync(UserModel model)
        {
            UserEntity userEntity = model.Adapt<UserEntity>();
            return _userRepository.UpdateAsync(userEntity);
        }
    }
}
