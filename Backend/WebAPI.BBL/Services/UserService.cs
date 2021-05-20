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

        public UserModel GetUser(int? id)
        {
            UserEntity user = _userRepository.FindFirst(
                user =>
                {
                    return id == user.Id;
                }
            );
            if (user == null)
            {
                return null;
            }
            return new UserModel { Id = user.Id, Email = user.Email };
        }

        public IEnumerable<UserModel> GetUsers()
        {
            return _userRepository.GetAll().Adapt<IEnumerable<UserModel>>();
        }

        public bool DeleteUser(int? id)
        {
            UserModel user = GetUser(id);
            if (user == null)
            {
                return false;
            }
            _userRepository.Delete(id.Value);
            return true;
        }

        public async Task<bool> EditUser(AuthentificationModel model)
        {
            byte[] salt = AuthentificationUtils.GenerateSalt();
            byte[] hash = AuthentificationUtils.ComputeHash(model.Password, salt);

            UserEntity userEntity = new UserEntity
            {
                Email = model.Email,
                PasswordHash = hash,
                Salt = salt,
                RoleId = (int)UserRoleModel.CommonUser
            };
            await _userRepository.Update(userEntity);
            return true;
        }
    }
}
