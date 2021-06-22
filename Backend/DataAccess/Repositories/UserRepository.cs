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
            // This measure is temporary. The directive will be removed with the release of EF 6.0
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return _context.Users.FirstOrDefaultAsync(
                user => user.Email == email
            );
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public ValueTask<UserEntity?> GetByAsync(int id)
        {
            // This measure is temporary. The directive will be removed with the release of EF 6.0
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return _context.Users.FindAsync(id);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public Task UpdateAsync(UserEntity user)
        {
            _context.Users.Update(user);
            return _context.SaveChangesAsync();
        }
    }
}
