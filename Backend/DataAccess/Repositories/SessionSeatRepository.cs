using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class SessionSeatRepository
    {
        private readonly CinemabooContext _context;

        public SessionSeatRepository(CinemabooContext context)
        {
            _context = context;
        }

        public Task CreateAsync(SessionSeatEntity seat)
        {
            _context.SessionSeats.Add(seat);
            return _context.SaveChangesAsync();
        }

        public Task CreateAsync(IReadOnlyCollection<SessionSeatEntity> entities)
        {
            _context.SessionSeats.AddRange(entities);
            return _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<SessionSeatEntity>> GetAllByAsync(int sessionId)
        {
            List<SessionSeatEntity> seats = await _context.SessionSeats
                .Where(seat => seat.SessionId == sessionId)
                .ToListAsync();
            return seats;
        }

        public async Task<SessionSeatEntity?> GetByAsync(int sessionId, int seatId)
        {
            SessionSeatEntity? entity = await _context.SessionSeats.FindAsync(sessionId, seatId);
            if (entity == null)
            {
                return entity;
            }
            return entity;
        }

        public int GetNumberOfFreeSeats(int sessionId)
        {
            return _context.SessionSeats
                .Where(seat => seat.SessionId == sessionId).Count();
        }

        public async Task<bool> DeleteByAsync(int id)
        {
            SessionSeatEntity? seat = await _context.SessionSeats.FindAsync(id);
            if (seat == null)
            {
                return false;
            }
            _context.SessionSeats.Remove(seat);
            return true;
        }

        public Task DeleteAllByAsync(int sessionId)
        {
            _context.SessionSeats.RemoveRange(_context.SessionSeats.Where(seat => seat.SessionId == sessionId));
            return _context.SaveChangesAsync();
        }

        public Task UpdateAsync(SessionSeatEntity seat)
        {
            _context.SessionSeats.Update(seat);
            return _context.SaveChangesAsync();
        }
    }
}
