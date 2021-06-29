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

        public async Task<int> CreateAsync(HallEntity hall)
        {
            _context.Halls.Add(hall);
            await _context.SaveChangesAsync();
            return hall.Id;
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

        public async Task<HallEntity?> GetByAsync(int id)
        {
            HallEntity? entity = await _context.Halls.FindAsync(id);
            if (entity == null)
            {
                return entity;
            }
            return entity;
        }

        public Task UpdateAsync(HallEntity hall)
        {
            _context.Halls.Update(hall);
            return _context.SaveChangesAsync();
        }
    }
}
