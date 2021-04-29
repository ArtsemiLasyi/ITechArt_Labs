using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DAL.Contexts;
using WebAPI.DAL.Entities;
using WebAPI.DAL.Interfaces;

namespace WebAPI.DAL.Repositories
{
    public class EFUserRepository : IRepository<Entities.UserEntity>
    {
        private readonly CinemabooContext _context;

        public EFUserRepository(CinemabooContext context)
        {
            _context = context;
        }

        public async void Create(UserEntity user)
        {
            _context
                .Users
                .Add(user);
            await _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            UserEntity user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
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

        public async void Update(UserEntity user)
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
