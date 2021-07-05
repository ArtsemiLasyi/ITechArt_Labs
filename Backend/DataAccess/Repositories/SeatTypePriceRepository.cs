using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class SeatTypePriceRepository
    {
        private readonly CinemabooContext _context;

        public SeatTypePriceRepository(CinemabooContext context)
        {
            _context = context;
        }

        public Task<SeatTypePriceEntity?> GetByAsync(int sessionId, int seatTypeId)
        {
            // This measure is temporary. The directive will be removed with the release of EF 6.0
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return _context.SeatTypePrices
                .Include("Currencies")
                .Include("SeatTypes")
                .FirstOrDefaultAsync(
                    seatTypePrice =>
                        seatTypePrice.SeatTypeId == seatTypeId
                        && seatTypePrice.SessionId == sessionId
                );
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public async Task<IReadOnlyCollection<SeatTypePriceEntity>> GetAllByAsync(int sessionId)
        {
            List<SeatTypePriceEntity> entities = await _context.SeatTypePrices
                .Where(seatTypePrice => seatTypePrice.SessionId == sessionId)
                .ToListAsync();
            return entities;
        }

        public Task CreateAsync(SeatTypePriceEntity entity)
        {
            _context.SeatTypePrices.Add(entity);
            return _context.SaveChangesAsync();
        }

        public Task UpdateAsync(SeatTypePriceEntity entity)
        {
            _context.SeatTypePrices.Update(entity);
            return _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteByAsync(int serviceId, int cinemaId)
        {
            SeatTypePriceEntity? service = await GetByAsync(serviceId, cinemaId);
            if (service != null)
            {
                _context.SeatTypePrices.Remove(service);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
