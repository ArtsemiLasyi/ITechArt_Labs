using DataAccess.Contexts;
using DataAccess.Entities;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PosterRepository
    {
        private readonly CinemabooContext _context;

        public PosterRepository(CinemabooContext context)
        {
            _context = context;
        }

        public Task CreateAsync(PosterEntity poster)
        {
            _context.Posters.Add(poster);
            return _context.SaveChangesAsync();
        }

        public ValueTask<PosterEntity?> GetByAsync(int id)
        {
            // This measure is temporary. The directive will be removed with the release of EF 6.0
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return _context.Posters.FindAsync(id);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public Task UpdateAsync(PosterEntity poster)
        {
            _context.Update(poster);
            return _context.SaveChangesAsync();
        }
    }
}
