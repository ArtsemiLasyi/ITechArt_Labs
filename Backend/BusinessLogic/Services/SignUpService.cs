using BusinessLogic.Models;
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
        private readonly PasswordRepository _passwordRepository;
        private readonly PasswordService _passwordService;

        public SignUpService(
            UserRepository userRepository,
            PasswordRepository passwordRepository,
            PasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordRepository = passwordRepository;
            _passwordService = passwordService;
        }

        public async Task<bool> SignUpAsync(SignUpModel model)
        {
            UserEntity? userEntity = _userRepository.GetByEmail(model.Email);
            if (userEntity != null)
            {
                return false;
            }

            PasswordModel passwordModel = _passwordService.GetPasswordModel(model.Password);

            userEntity = new UserEntity
            {
                Email = model.Email,
                RoleId = (int)UserRoleModel.CommonUser
            };
            UserPasswordEntity passwordEntity = new UserPasswordEntity
            {
                UserId = userEntity.Id,
                PasswordHash = passwordModel.PasswordHash,
                Salt = passwordModel.Salt
            };

            await _userRepository.CreateAsync(userEntity);
            await _passwordRepository.CreateAsync(passwordEntity);
            return true;
        }
    }
}
