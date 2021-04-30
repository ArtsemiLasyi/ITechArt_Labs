using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Utils;
using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class AuthentificationService : IAuthentificationService
    {
        private IUnitOfWork Database { get; set; }

        public AuthentificationService(IUnitOfWork unitofwork)
        {
            Database = unitofwork;
        }

        public bool SignIn(AuthentificationModel model)
        {
            SHA256 mySHA256 = SHA256.Create();

            UserEntity userEntity = Database.Users.FindFirst(
                user =>
                {
                    return user.Email == model.Email;
                }
            );
            if (userEntity == null)
            {
                return false;
            }

            string salt = userEntity.Salt;
            byte[] bytePassword = Encoding.ASCII.GetBytes(model.Password + salt);
            byte[] hashValue = mySHA256.ComputeHash(bytePassword);

            bool equal = AuthentificationUtils.CompareHashes(
                hashValue,
                userEntity.PasswordHash
            );
            return equal;
        }

        public void SignUp(AuthentificationModel model)
        {
            SHA256 mySHA256 = SHA256.Create();
            byte[] salt = AuthentificationUtils.GenerateSalt();
            string saltStr = Encoding.Unicode.GetString(salt);
            byte[] bytePassword = Encoding.ASCII.GetBytes(model.Password + salt);
            byte[] hashValue = mySHA256.ComputeHash(bytePassword);

            UserEntity userEntity = new UserEntity
            {
                Email = model.Email,
                PasswordHash = hashValue,
                Salt = saltStr,
                RoleId = (int)UserRoleModel.CommonUser
            };
            Database.Users.Create(userEntity);
        }
    }
}
