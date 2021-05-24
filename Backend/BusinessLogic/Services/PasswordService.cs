using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class PasswordService
    {
        public static readonly int SALT_LENGTH = 20;
        private readonly PasswordRepository _passwordRepository;

        public PasswordService(PasswordRepository repository)
        {
            _passwordRepository = repository;
        }

        public async Task SetPassword(int id, string password)
        {
            PasswordModel model = GetPasswordModel(password);
            UserPasswordEntity entity = new UserPasswordEntity
            {
                UserId = id,
                PasswordHash = model.PasswordHash,
                Salt = model.Salt
            };
            await _passwordRepository.UpdateAsync(entity);
        }

        public PasswordModel GetPasswordModel(string password)
        {
            byte[] salt = GenerateSalt();
            byte[] hash = ComputeHash(password, salt);

            return new PasswordModel
            {
                PasswordHash = hash,
                Salt = salt
            };
        }

        private static byte[] GenerateSalt()
        {
            using (RNGCryptoServiceProvider rncCsp = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[SALT_LENGTH];
                rncCsp.GetBytes(salt);
                return salt;
            }
        }

        public static byte[] ComputeHash(string password, byte[] salt)
        {
            using (SHA256 mySHA256 = SHA256.Create())
            {
                byte[] passwordByte = Encoding.ASCII.GetBytes(password);
                byte[] passwordWithSalt = passwordByte.Concat(salt).ToArray();
                byte[] hash = mySHA256.ComputeHash(passwordWithSalt);
                return hash;
            }
        }
    }
}
