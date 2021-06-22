using DataAccess.Contexts;
using DataAccess.Entities;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class HallPhotoRepository
    {
        private readonly CinemabooContext _context;

        public HallPhotoRepository(CinemabooContext context)
        {
            _context = context;
        }

        public Task CreateAsync(HallPhotoEntity photo)
        {
            _context.HallPhotos.Add(photo);
            return _context.SaveChangesAsync();
        }

        public ValueTask<HallPhotoEntity?> GetByAsync(int id)
        {
            // This measure is temporary. The directive will be removed with the release of EF 6.0
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return _context.HallPhotos.FindAsync(id);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public Task UpdateAsync(HallPhotoEntity photo)
        {
            _context.HallPhotos.Update(photo);
            return _context.SaveChangesAsync();
        }
    }
}
