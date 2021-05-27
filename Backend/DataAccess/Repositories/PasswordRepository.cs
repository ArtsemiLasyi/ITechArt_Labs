using DataAccess.Contexts;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PasswordRepository
    {
        private readonly CinemabooContext _context;

        public PasswordRepository(CinemabooContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(UserPasswordEntity entity)
        {
            _context
                .UserPasswords
                .Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByAsync(int userId)
        {
            UserPasswordEntity? entity = _context.UserPasswords.Find(userId);
            if (entity != null)
            {
                _context.UserPasswords.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public UserPasswordEntity? GetBy(int userId)
        {
            UserPasswordEntity? userPasswordEntity = _context.UserPasswords.FirstOrDefault(
                entity => entity.UserId == userId
            );
            return userPasswordEntity;
        }

        public async Task UpdateAsync(UserPasswordEntity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
