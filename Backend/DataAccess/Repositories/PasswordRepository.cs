﻿using DataAccess.Contexts;
using DataAccess.Entities;
using System.Linq;
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

        public Task CreateAsync(UserPasswordEntity entity)
        {
            _context.UserPasswords.Add(entity);
            return _context.SaveChangesAsync();
        }

        public async Task DeleteByAsync(int userId)
        {
            UserPasswordEntity? entity = await _context.UserPasswords.FindAsync(userId);
            if (entity != null)
            {
                _context.UserPasswords.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public ValueTask<UserPasswordEntity> GetByAsync(int userId)
        {
            return _context.UserPasswords.FindAsync(userId);
        }

        public Task UpdateAsync(UserPasswordEntity entity)
        {
            _context.UserPasswords.Update(entity);
            return _context.SaveChangesAsync();
        }
    }
}
