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

        public async Task DeleteAsync(int id)
        {
            UserPasswordEntity entity = _context.UserPasswords.Find(id);
            if (entity != null)
            {
                _context.UserPasswords.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public UserPasswordEntity? GetById(int id)
        {
            UserPasswordEntity? userPasswordEntity = _context.UserPasswords.FirstOrDefault(
                entity => entity.UserId == id
            );
            return userPasswordEntity;
        }

        public UserPasswordEntity Get(int id)
        {
            return _context
                .UserPasswords
                .Find(id);
        }

        public async Task UpdateAsync(UserPasswordEntity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
