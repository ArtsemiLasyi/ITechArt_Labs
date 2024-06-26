﻿using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public Task CreateAsync(IReadOnlyCollection<SeatEntity> entities)
        {
            _context.Seats.AddRange(entities);
            return _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<SeatEntity>> GetAllBy(int hallId)
        {
            List<SeatEntity> seats = await _context.Seats
                .Include("SeatType")
                .Where(seat => seat.HallId == hallId && !seat.IsDeleted)
                .ToListAsync();
            return seats;
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

        public async Task DeleteAllByAsync(int hallId)
        {
            IReadOnlyCollection<SeatEntity> seats = await GetAllBy(hallId);
            _context.Seats.UpdateRange(
                seats.Select(
                    seat => 
                    {
                        seat.IsDeleted = true;
                        return seat;
                    }
                )
            );
            await _context.SaveChangesAsync();
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
