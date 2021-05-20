using BusinessLogic.Models;
using BusinessLogic.Utils;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Mapster;

namespace BusinessLogic.Services
{
    public class AuthentificationService
    {
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthentificationService(UserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        private UserEntity GetUserByEmail(string email)
        {
            UserEntity userEntity = _userRepository.FindFirst(
                user =>
                {
                    return user.Email == email;
                }
            );
            return userEntity;
        }

        public bool SignIn(AuthentificationModel model)
        {

            UserEntity userEntity = GetUserByEmail(model.Email);
            if (userEntity == null)
            {
                return false;
            }

            byte[] hash = AuthentificationUtils.ComputeHash(model.Password, userEntity.Salt);
            bool equal = Enumerable.SequenceEqual(hash, userEntity.PasswordHash);
            UserModel userModel = userEntity.Adapt<UserModel>();
            if (equal)
            {
                AuthentificationUtils.GenerateJwtToken(userModel, _configuration);
            }
            return equal;
        }

        public async Task<bool> SignUp(AuthentificationModel model)
        {
            UserEntity userEntity = GetUserByEmail(model.Email);
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
