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

        public async Task CreateAsync(UserPasswordEntity entity)
        {
            _context.UserPasswords.Add(entity);
            _context.SaveChangesAsync();
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

        public async Task<UserPasswordEntity?> GetByAsync(int userId)
        {
            UserPasswordEntity? userPasswordEntity = await _context.UserPasswords.FindAsync(userId);
            return userPasswordEntity;
        }

        public async Task UpdateAsync(UserPasswordEntity entity)
        {
            _context.Update(entity);
            _context.SaveChangesAsync();
        }
    }
}