using BusinessLogic.Models;
using BusinessLogic.Results;
using DataAccess.Entities;
using DataAccess.Repositories;
using Mapster;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class SignInService
    {
        private readonly UserRepository _userRepository;
        private readonly PasswordRepository _passwordRepository;
        private readonly PasswordService _passwordService;

        public SignInService(
            UserRepository userRepository,
            PasswordRepository passwordRepository,
            PasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordRepository = passwordRepository;
            _passwordService = passwordService;
        }

        public SignInResult SignIn(SignInModel model)
        {
            UserEntity? userEntity = _userRepository.GetByEmail(model.Email);
            if (userEntity == null)
            {
                return new SignInResult
                { 
                    IsSuccessful = false 
                };
            }
            UserPasswordEntity? passwordEntity = _passwordRepository.GetById(userEntity.Id);
            if (passwordEntity == null)
            {
                return new SignInResult
                {
                    IsSuccessful = false
                };
            }

            byte[] hash = PasswordService.ComputeHash(model.Password, passwordEntity.Salt);
            bool equal = Enumerable.SequenceEqual(hash, passwordEntity.PasswordHash);
            UserModel userModel = userEntity.Adapt<UserModel>();
            if (equal)
            {
                return new SignInResult
                {
                    IsSuccessful = true,
                    User = userModel
                };
            }
            return new SignInResult
            {
                IsSuccessful = false
            };
        }
    }
}
