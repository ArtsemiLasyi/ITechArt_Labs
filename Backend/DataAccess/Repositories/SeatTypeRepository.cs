using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class SeatTypeRepository
    {
        private readonly CinemabooContext _context;
        public SeatTypeRepository(CinemabooContext context)
        {
            _context = context;
        }

        public Task CreateAsync(SeatTypeEntity seat)
        {
            _context.SeatTypes.Add(seat);
            return _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteByAsync(int id)
        {
            SeatTypeEntity? seatType = await _context.SeatTypes.FindAsync(id);
            if (seatType == null)
            {
                return false;
            }
            _context.SeatTypes.Remove(seatType);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IReadOnlyCollection<SeatTypeEntity>> GetAllAsync()
        {
            List<SeatTypeEntity> seatTypes = await _context.SeatTypes.ToListAsync();
            return seatTypes;
        }

        public ValueTask<SeatTypeEntity?> GetByAsync(int id)
        {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return _context.SeatTypes.FindAsync(id);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public Task UpdateAsync(SeatTypeEntity seatType)
        {
            _context.SeatTypes.Update(seatType);
            return _context.SaveChangesAsync();
        }

        public async Task<bool> IsUsedAsync(int id)
        {
            SeatEntity? entity = await _context.Seats.FirstOrDefaultAsync(seat => seat.SeatTypeId == id);
            if (entity != null)
            {
                return true;
            }
            return false;
        }
    }
}
