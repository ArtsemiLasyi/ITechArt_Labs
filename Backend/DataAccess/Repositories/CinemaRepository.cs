using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CinemaRepository
    {
        private readonly CinemabooContext _context;

        public CinemaRepository(CinemabooContext context)
        {
            _context = context;
        }

        public Task CreateAsync(CinemaEntity film)
        {
            _context.Cinemas.Add(film);
            return _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteByAsync(int id)
        {
            CinemaEntity? cinema = await _context.Cinemas.FindAsync(id);
            if (cinema?.IsDeleted == false)
            {
                cinema.IsDeleted = true;
                _context.Update(cinema);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IReadOnlyCollection<CinemaEntity>> GetAllByAsync(int cityId)
        {
            List<CinemaEntity> cinemas = await _context.Cinemas
                .Where(cinema => !cinema.IsDeleted && cinema.CityId == cityId)
                .ToListAsync();
            return cinemas;
        }

        public ValueTask<CinemaEntity?> GetByAsync(int id)
        {
            // This measure is temporary. The directive will be removed with the release of EF 6.0
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return _context.Cinemas.FindAsync(id);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public Task UpdateAsync(CinemaEntity cinema)
        {
            _context.Cinemas.Update(cinema);
            return _context.SaveChangesAsync();
        }
    }
}
