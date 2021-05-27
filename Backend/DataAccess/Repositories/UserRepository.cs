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

        public async Task<UserEntity> CreateAsync(UserEntity user)
        {
            _context
                .Users
                .Add(user);
            await _context.SaveChangesAsync();
            return GetBy(user.Email);
        }

        public async Task<bool> DeleteByAsync(int id)
        {
            UserEntity? user = _context.Users.Find(id);
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

        public UserEntity? GetBy(string email)
        {
            UserEntity? userEntity = _context.Users.FirstOrDefault(
                user => user.Email == email
            );
            return userEntity;
        }

        public UserEntity? GetBy(int id)
        {
            UserEntity? userEntity = _context.Users.FirstOrDefault(
                user => user.Id == id
            );
            return userEntity;
        }

        public async Task UpdateAsync(UserEntity user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
