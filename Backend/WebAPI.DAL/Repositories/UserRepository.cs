using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class UserRepository
    {
        private readonly CinemabooContext _context;

        public UserRepository(CinemabooContext context)
        {
            _context = context;
        }

        public async Task Create(UserEntity user)
        {
            _context
                .Users
                .Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            UserEntity user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<UserEntity> Find(Func<UserEntity, bool> predicate)
        {
            return _context
                .Users
                .Where(predicate);
        }

        public UserEntity Get(int id)
        {
            return _context
                .Users
                .Find(id);
        }

        public IEnumerable<UserEntity> GetAll()
        {
            return _context.Users;
        }

        public async Task Update(UserEntity user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public UserEntity FindFirst(Func<UserEntity, bool> predicate)
        {
            return _context
                .Users
                .FirstOrDefault(predicate);
        }
    }
}
