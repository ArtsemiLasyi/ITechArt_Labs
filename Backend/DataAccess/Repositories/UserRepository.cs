using System.Threading.Tasks;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class UserRepository
    {
        private readonly CinemabooContext _context;

        public UserRepository(CinemabooContext context)
        {
            _context = context;
        }

        public Task CreateAsync(UserEntity user)
        {
            _context.Users.Add(user);
            return _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteByAsync(int id)
        {
            UserEntity? user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task<UserEntity?> GetByAsync(string email)
        {
            return _context.Users.FirstOrDefaultAsync(
                user => user.Email == email
            );
        }

        public Task<UserEntity?> GetByAsync(int id)
        {
            return _context.Users.FindAsync(id);
        }

        public Task UpdateAsync(UserEntity user)
        {
            _context.Update(user);
            return _context.SaveChangesAsync();
        }
    }
}
