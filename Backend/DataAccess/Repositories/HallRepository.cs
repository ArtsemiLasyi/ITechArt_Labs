using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class HallRepository
    {
        private readonly CinemabooContext _context;

        public HallRepository(CinemabooContext context)
        {
            _context = context;
        }

        public Task CreateAsync(HallEntity hall)
        {
            _context.Halls.Add(hall);
            return _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteByAsync(int id)
        {
            HallEntity? hall = await _context.Halls.FindAsync(id);
            if (hall?.IsDeleted == false)
            {
                hall.IsDeleted = true;
                _context.Update(hall);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IReadOnlyCollection<HallEntity>> GetAllByAsync(int cinemaId)
        {
            List<HallEntity> halls = await _context.Halls
                .Where(hall => !hall.IsDeleted && hall.CinemaId == cinemaId)
                .ToListAsync();
            return halls;
        }

        public ValueTask<HallEntity?> GetByAsync(int id)
        {
            // This measure is temporary. The directive will be removed with the release of EF 6.0
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return _context.Halls.FindAsync(id);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public Task UpdateAsync(HallEntity hall)
        {
            _context.Halls.Update(hall);
            return _context.SaveChangesAsync();
        }
    }
}
