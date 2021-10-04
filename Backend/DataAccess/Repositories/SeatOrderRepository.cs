using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class SeatOrderRepository
    {
        private readonly CinemabooContext _context;

        public SeatOrderRepository(CinemabooContext context)
        {
            _context = context;
        }

        public Task CreateAsync(SeatOrderEntity entity)
        {
            _context.SeatOrders.Add(entity);
            return _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteByAsync(int orderId, int seatId)
        {
            SeatOrderEntity? entity = await GetByAsync(orderId, seatId);
            if (entity != null)
            {
                _context.SeatOrders.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Task<SeatOrderEntity?> GetByAsync(int orderId, int seatId)
        {
            // This measure is temporary. The directive will be removed with the release of EF 6.0
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return _context.SeatOrders
                .Include("Seat")
                .FirstOrDefaultAsync(
                    seatOrder => seatOrder.SeatId == seatId && seatOrder.OrderId == orderId
                );
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public async Task<IReadOnlyCollection<SeatOrderEntity>> GetAllByAsync(int orderId)
        {
            List<SeatOrderEntity> entities = await _context.SeatOrders
                .Include("Seat")
                .Where(seatOrder => seatOrder.OrderId == orderId)
                .ToListAsync();
            return entities;
        }
    }
}
