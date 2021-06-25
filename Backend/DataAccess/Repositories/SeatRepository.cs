using DataAccess.Contexts;
using DataAccess.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class SeatRepository
    {
        private readonly CinemabooContext _context;

        public SeatRepository(CinemabooContext context)
        {
            _context = context;
        }

        public Task CreateAsync(SeatEntity seat)
        {
            _context.Seats.Add(seat);
            return _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteByAsync(int id)
        {
            SeatEntity? seat = await _context.Seats.FindAsync(id);
            if (seat == null)
            {
                return false;    
            }
            _context.Seats.Remove(seat);
            return true;
        }

        public Task DeleteAllByAsync(int hallId)
        {
            _context.Seats.RemoveRange(_context.Seats.Where(seat => seat.HallId == hallId));
            return _context.SaveChangesAsync();
        }

        public async Task<SeatEntity?> GetByAsync(int id)
        {
            SeatEntity? entity = await _context.Seats.FindAsync(id);
            if (entity == null)
            {
                return entity;
            }
            await _context.Entry(entity).Reference(seat => seat.SeatType).LoadAsync();
            return entity;
        }

        public Task UpdateAsync(SeatEntity seat)
        {
            _context.Seats.Update(seat);
            return _context.SaveChangesAsync();
        }
    }
}
