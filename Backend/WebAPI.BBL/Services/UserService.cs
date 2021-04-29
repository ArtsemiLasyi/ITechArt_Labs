using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BBL.BusinessModels;
using WebAPI.BBL.DTOs;
using WebAPI.BBL.Interfaces;
using WebAPI.DAL.Entities;
using WebAPI.DAL.Interfaces;

namespace WebAPI.BBL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork unitofwork)
        {
            Database = unitofwork;
        }

        public bool SignInUser(UserDTO userDto)
        {
            SHA256 mySHA256 = SHA256.Create();

            UserEntity userEntity = Database.Users.FindFirst(
                user =>
                {
                    return user.Email == userDto.Email;
                }
            );
            if (userEntity == null)
            {
                return false;
            }

            string salt = userEntity.Salt;
            byte[] bytePassword = Encoding.ASCII.GetBytes(userDto.Password + salt);
            byte[] hashValue = mySHA256.ComputeHash(bytePassword);

            bool equal = AuthentificationUtils.CompareHashes(
                hashValue, 
                userEntity.PasswordHash
            );
            return equal;
        }

        public void SignUpUser(UserDTO userDto)
        {
            SHA256 mySHA256 = SHA256.Create();
            byte[] salt = AuthentificationUtils.GenerateSalt();
            string saltStr = Encoding.Unicode.GetString(salt);
            byte[] bytePassword = Encoding.ASCII.GetBytes(userDto.Password + salt);
            byte[] hashValue = mySHA256.ComputeHash(bytePassword);

            UserEntity userEntity = new UserEntity
            {
                Email = userDto.Email,
                PasswordHash = hashValue,
                Salt = saltStr,
                RoleId = (int)UserRoleDTO.CommonUser
            };
            Database.Users.Create(userEntity);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public UserDTO GetUser(int? id)
        {
            UserEntity user = Database.Users.FindFirst(user => id == user.Id);
            if (user == null)
            {
                return null;
            }
            return new UserDTO { Id = user.Id, Email = user.Email };
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            IMapper mapper = new MapperConfiguration(
                cfg =>
                { 
                    cfg.CreateMap<UserEntity, UserDTO>();
                }
            ).CreateMapper();
            return mapper.Map<IEnumerable<UserEntity>, List<UserDTO>>(Database.Users.GetAll());
        }

        bool IUserService.DeleteUser(int? id)
        {
            UserDTO user = GetUser(id);
            if (user == null)
            {
                return false;
            }
            Database.Users.Delete(id.Value);
            return true;
        }

        bool IUserService.EditUser(UserDTO userDto)
        {
            SHA256 mySHA256 = SHA256.Create();
            byte[] salt = AuthentificationUtils.GenerateSalt();
            string saltStr = Encoding.Unicode.GetString(salt);
            byte[] bytePassword = Encoding.ASCII.GetBytes(userDto.Password + salt);
            byte[] hashValue = mySHA256.ComputeHash(bytePassword);

            UserEntity userEntity = new UserEntity
            {
                Email = userDto.Email,
                PasswordHash = hashValue,
                Salt = saltStr,
                RoleId = (int)UserRoleDTO.CommonUser
            };
            Database.Users.Update(userEntity);
            return true;
        }
    }
}
