using BusinessLogic.Models;
using BusinessLogic.Utils;
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
        private readonly IConfiguration _configuration;

        public SignInService(UserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public (bool, string) SignIn(SignInModel model)
        {
            string token = null;
            UserEntity? userEntity = _userRepository.GetByEmail(model.Email);
            if (userEntity == null)
            {
                return (false, token);
            }

            byte[] hash = AuthentificationUtils.ComputeHash(model.Password, userEntity.Salt);
            bool equal = Enumerable.SequenceEqual(hash, userEntity.PasswordHash);
            UserModel userModel = userEntity.Adapt<UserModel>();
            if (equal)
            {
                JwtSecurityToken jwtSecurityToken = AuthentificationUtils.GenerateJwtToken(userModel, _configuration);
                token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            }
            return (equal, token);
        }
    }
}
