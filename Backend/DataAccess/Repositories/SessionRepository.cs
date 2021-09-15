﻿using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
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

        public Task CreateAsync(SessionEntity session)
        {
            int freeSeatsNumber = _context.Seats.Where(seat => seat.HallId == session.HallId).Count();
            session.FreeSeatsNumber = freeSeatsNumber;
            _context.Sessions.Add(session);
            return _context.SaveChangesAsync();
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
                .ToListAsync();
            return sessions;
        }

        public async Task<IReadOnlyCollection<SessionEntity>> GetAllByAsync(int filmId)
        {
            List<SessionEntity> sessions = await _context.Sessions
                .Where(
                    session =>
                        !session.IsDeleted
                        && session.FilmId == filmId
                )
                .ToListAsync();
            return sessions;
        }

        public ValueTask<SessionEntity?> GetByAsync(int id)
        {
            // This measure is temporary. The directive will be removed with the release of EF 6.0
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return _context.Sessions.FindAsync(id);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public Task UpdateAsync(SessionEntity session)
        {
            _context.Sessions.Update(session);
            return _context.SaveChangesAsync();
        }
    }
}
