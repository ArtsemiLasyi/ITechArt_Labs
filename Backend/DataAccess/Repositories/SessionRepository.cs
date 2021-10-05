using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Parameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class SessionRepository
    {
        private readonly CinemabooContext _context;

        public SessionRepository(CinemabooContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(SessionEntity session)
        {
            int freeSeatsNumber = await _context.Seats
                .Where(seat => seat.HallId == session.HallId)
                .CountAsync();
            session.FreeSeatsNumber = freeSeatsNumber;
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();
            return session.Id;
        }

        public async Task<bool> DeleteByAsync(int id)
        {
            SessionEntity? session = await _context.Sessions.FindAsync(id);
            if (session?.IsDeleted == false)
            {
                session.IsDeleted = true;
                _context.Update(session);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IReadOnlyCollection<SessionEntity>> GetAllByAsync(int hallId, int filmId)
        {
            List<SessionEntity> sessions = await _context.Sessions
                .Where(
                    session => 
                        !session.IsDeleted 
                        && session.HallId == hallId 
                        && session.FilmId == filmId
                )
                .Include(session => session.Hall)
                .Include(session => session.Film)
                .OrderBy(session => session.StartDateTime)
                .ToListAsync();
            return sessions;
        }

        public async Task<IReadOnlyCollection<SessionEntity>> GetAllByAsync(int cinemaId, SessionEntitySearchParameters parameters)
        {
            IQueryable<SessionEntity> query = _context.Sessions
                .Include(session => session.Hall)
                .Include(session => session.Film)
                .Where(
                    session =>
                        !session.IsDeleted
                        && session.Hall.CinemaId == cinemaId
                );

            if (parameters.FirstSessionDateTime != null)
            {
                query = query.Where(
                    session => session.StartDateTime >= parameters.FirstSessionDateTime
                );
            }

            if (parameters.LastSessionDateTime != null)
            {
                query = query.Where(
                    session => session.StartDateTime <= parameters.LastSessionDateTime
                );
            }

            if (parameters.FreeSeatsNumber != null)
            {
                query = query.Where(
                    session => session.FreeSeatsNumber >= parameters.FreeSeatsNumber
                );
            }
            if (parameters.FilmId != null)
            {
                query = query.Where(
                    session => session.FilmId == parameters.FilmId
                );
            }
            query = query.OrderBy(session => session.StartDateTime);
            List<SessionEntity> sessions = await query.ToListAsync();
            return sessions;
        }

        public Task<SessionEntity?> GetByAsync(int id)
        {
            // This measure is temporary. The directive will be removed with the release of EF 6.0
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return _context.Sessions
                .Include(session => session.Hall)
                .Include(session => session.Film)
                .FirstAsync(session => session.Id == id);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public Task UpdateAsync(SessionEntity session)
        {
            _context.Sessions.Update(session);
            return _context.SaveChangesAsync();
        }
    }
}
