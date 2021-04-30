using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Utils;
using BusinessLogic.Models;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Mapster;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork unitofwork)
        {
            Database = unitofwork;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public UserModel GetUser(int? id)
        {
            UserEntity user = Database.Users.FindFirst(user => id == user.Id);
            if (user == null)
            {
                return null;
            }
            return new UserModel { Id = user.Id, Email = user.Email };
        }

        public IEnumerable<UserModel> GetUsers()
        {
            return Database.Users.GetAll().Adapt<IEnumerable<UserModel>>();
        }

        public bool DeleteUser(int? id)
        {
            UserModel user = GetUser(id);
            if (user == null)
            {
                return false;
            }
            Database.Users.Delete(id.Value);
            return true;
        }

        public bool EditUser(UserModel user, string password)
        {
            SHA256 mySHA256 = SHA256.Create();
            byte[] salt = AuthentificationUtils.GenerateSalt();
            string saltStr = Encoding.Unicode.GetString(salt);
            byte[] bytePassword = Encoding.ASCII.GetBytes(password + salt);
            byte[] hashValue = mySHA256.ComputeHash(bytePassword);

            UserEntity userEntity = new UserEntity
            {
                Email = user.Email,
                PasswordHash = hashValue,
                Salt = saltStr,
                RoleId = (int)UserRoleModel.CommonUser
            };
            Database.Users.Update(userEntity);
            return true;
        }
    }
}
