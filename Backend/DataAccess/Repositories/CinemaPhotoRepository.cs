using DataAccess.Contexts;
using DataAccess.Entities;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CinemaPhotoRepository
    {
        private readonly CinemabooContext _context;

        public CinemaPhotoRepository(CinemabooContext context)
        {
            _context = context;
        }

        public Task CreateAsync(CinemaPhotoEntity photo)
        {
            _context.CinemaPhotos.Add(photo);
            return _context.SaveChangesAsync();
        }

        public ValueTask<CinemaPhotoEntity?> GetByAsync(int id)
        {
            // This measure is temporary. The directive will be removed with the release of EF 6.0
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return _context.CinemaPhotos.FindAsync(id);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public Task UpdateAsync(CinemaPhotoEntity photo)
        {
            _context.CinemaPhotos.Update(photo);
            return _context.SaveChangesAsync();
        }
    }
}
