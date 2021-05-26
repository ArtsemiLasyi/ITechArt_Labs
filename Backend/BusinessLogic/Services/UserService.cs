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
            UserEntity? userEntity = new ();
            userEntity.RoleId = (int)UserRole.User;
            userEntity = await _userRepository.CreateAsync(userEntity);
            return userEntity.Adapt<UserModel>();
        }

        public UserModel? Get(string email)
        {
            UserEntity? user = _userRepository.Get(email);
            if (user == null)
            {
                return null;
            }
            return user.Adapt<UserModel>();
        }

        public UserModel? Get(int id)
        {
            UserEntity? user = _userRepository.Get(id);
            if (user == null)
            {
                return null;
            }
            return user.Adapt<UserModel>(); 
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> EditAsync(UserModel model)
        {
            UserEntity userEntity = model.Adapt<UserEntity>();
            await _userRepository.UpdateAsync(userEntity);
            return true;
        }
    }
}
