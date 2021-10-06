using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class SessionSeatRepository
    {
        private readonly CinemabooContext _context;
        private readonly SeatOptions _seatSnapshotOptions;

        public SessionSeatRepository(
            CinemabooContext context,
            IOptionsSnapshot<SeatOptions> seatOptionsSnapshotAssessor)
        {
            _context = context;
            _seatSnapshotOptions = seatOptionsSnapshotAssessor.Value;
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
                .Include(sessionSeat => sessionSeat.Seat)
                .ThenInclude(seat => seat.SeatType)
                .AsNoTracking()
                .Where(sessionSeat => sessionSeat.SessionId == sessionId)
                .ToListAsync();
            return seats;
        }

        public async Task<SessionSeatEntity?> GetByAsync(int sessionId, int seatId)
        {
            SessionSeatEntity? entity = await _context.SessionSeats
                .Include(sessionSeat => sessionSeat.Seat)
                .ThenInclude(seat => seat.SeatType)
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    sessionSeat => sessionSeat.SessionId == sessionId && sessionSeat.SeatId == seatId
                );
            return entity;
        }

        public Task<int> GetNumberOfFreeSeatsAsync(int sessionId)
        {
            return _context.SessionSeats
                .Where(seat => seat.SessionId == sessionId)
                .CountAsync();
        }

        public async Task<bool> DeleteByAsync(int id)
        {
            SessionSeatEntity? seat = await _context.SessionSeats.FindAsync(id);
            if (seat == null)
            {
                return false;
            }
            _context.SessionSeats.Remove(seat);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task DeleteAllByAsync(int sessionId)
        {
            _context.SessionSeats.RemoveRange(_context.SessionSeats.Where(seat => seat.SessionId == sessionId));
            return _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SessionSeatEntity seat)
        {
            SessionSeatEntity entity = await _context.SessionSeats
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    sessionSeat => 
                        sessionSeat.SessionId == seat.SessionId && sessionSeat.SeatId == seat.SeatId
                );
            seat.TakenAt = DateTime.UtcNow;
            _context.SessionSeats.Update(seat);
            SessionEntity session = await _context.Sessions.FindAsync(seat.SessionId);
            if (seat.Status == 0)
            {
                session.FreeSeatsNumber++;
            }
            if (seat.Status == 1)
            {
                session.FreeSeatsNumber--;
            }
            _context.Sessions.Update(session);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStatusesAsync(int sessionId, IReadOnlyCollection<SessionSeatEntity> entities)
        {
            TimeSpan interval = _seatSnapshotOptions.SeatOccupancyInterval;
           
            List<SessionSeatEntity> seats = await _context.SessionSeats
                .Where(
                    sessionSeat => 
                        sessionSeat.Status == 1
                            && sessionSeat.SessionId == sessionId
                 )
                .ToListAsync();
            foreach (SessionSeatEntity seat in seats)
            {
                if (DateTime.UtcNow - seat.TakenAt >= interval)
                {
                    seat.Status = 0;
                }
            }
            _context.SessionSeats.UpdateRange(seats);
            await _context.SaveChangesAsync();
        }
    }
}
