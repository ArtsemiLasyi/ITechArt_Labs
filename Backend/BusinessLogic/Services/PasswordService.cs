using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
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

        public async Task CreateAsync(int userId, string password)
        {
            UserPasswordModel passwordModel = GetPasswordModel(password);
            UserPasswordEntity passwordEntity = new UserPasswordEntity
            {
                UserId = userId,
                PasswordHash = passwordModel.PasswordHash,
                Salt = passwordModel.Salt
            };

            _passwordRepository.CreateAsync(passwordEntity);
        }

        public async Task<bool> MatchForUserAsync(int userId, string password)
        {
            UserPasswordEntity? passwordEntity = await _passwordRepository.GetByAsync(userId);
            if (passwordEntity == null)
            {
                return false;
            }

            byte[] hash = ComputeHash(password, passwordEntity.Salt);
            bool equal = Enumerable.SequenceEqual(hash, passwordEntity.PasswordHash);
            return equal;
        }

        public async Task UpdateAsync(int userId, string password)
        {
            UserPasswordModel model = GetPasswordModel(password);
            UserPasswordEntity entity = new UserPasswordEntity
            {
                UserId = userId,
                PasswordHash = model.PasswordHash,
                Salt = model.Salt
            };
            _passwordRepository.UpdateAsync(entity);
        }

        public async Task DeleteByAsync(int userId)
        {
            _passwordRepository.DeleteByAsync(userId);
        }

        private UserPasswordModel GetPasswordModel(string password)
        {
            byte[] salt = GenerateSalt();
            byte[] hash = ComputeHash(password, salt);

            return new UserPasswordModel
            {
                PasswordHash = hash,
                Salt = salt
            };
        }

        private byte[] GenerateSalt()
        {
            using (RNGCryptoServiceProvider rncCsp = new())
            {
                byte[] salt = new byte[SALT_LENGTH];
                rncCsp.GetBytes(salt);
                return salt;
            }
        }

        private byte[] ComputeHash(string password, byte[] salt)
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
