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
                .Where(seat => seat.SessionId == sessionId)
                .ToListAsync();
            return seats;
        }

        public async Task<SessionSeatEntity?> GetByAsync(int sessionId, int seatId)
        {
            SessionSeatEntity? entity = await _context.SessionSeats.FindAsync(sessionId, seatId);
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
            _context.SessionSeats.Update(seat);
            SessionEntity session = await _context.Sessions.FindAsync(seat.SessionId);
            session.FreeSeatsNumber--;
            _context.Sessions.Update(session);
            await _context.SaveChangesAsync();
        }

        public Task UpdateStatusesAsync()
        {
            IQueryable<SessionSeatEntity> seats = _context.SessionSeats
                .Where(
                    sessionSeat => 
                        sessionSeat.Status == 1
                            && DateTime.UtcNow - sessionSeat.TakenAt > _seatSnapshotOptions.SeatOccupancyInterval
                 );
            _context.SessionSeats.UpdateRange(seats);
            return _context.SaveChangesAsync();
        }
    }
}
