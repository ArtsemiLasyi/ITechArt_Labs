using System.Linq;
using System.Text;
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

        public async Task<UserEntity?> CreateAsync(UserEntity user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return await GetByAsync(user.Email);
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

        public async Task<UserEntity?> GetByAsync(string email)
        {
            UserEntity? userEntity = await _context.Users.FirstOrDefaultAsync(
                user => user.Email == email
            );
            return userEntity;
        }

        public async Task<UserEntity?> GetByAsync(int id)
        {
            UserEntity? userEntity = await _context.Users.FindAsync(id);
            return userEntity;
        }

        public async Task UpdateAsync(UserEntity user)
        {
            _context.Update(user);
            _context.SaveChangesAsync();
        }
    }
}
