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
            userEntity = await _userRepository.CreateAsync(userEntity);
            return userEntity.Adapt<UserModel>();
        }

        public UserModel? GetBy(string email)
        {
            UserEntity? user = _userRepository.GetBy(email);
            if (user == null)
            {
                return null;
            }
            return user.Adapt<UserModel>();
        }

        public UserModel? GetBy(int id)
        {
            UserEntity? user = _userRepository.GetBy(id);
            if (user == null)
            {
                return null;
            }
            return user.Adapt<UserModel>(); 
        }

        public async Task<bool> DeleteByAsync(int id)
        {
           return await _userRepository.DeleteByAsync(id);
        }

        public void Edit(UserModel model)
        {
            UserEntity userEntity = model.Adapt<UserEntity>();
            _userRepository.Update(userEntity);
        }
    }
}
