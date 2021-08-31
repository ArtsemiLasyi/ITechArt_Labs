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
    public class OrderRepository
    {
        private readonly CinemabooContext _context;

        public OrderRepository(CinemabooContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(OrderEntity order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }

        public async Task<bool> DeleteByAsync(int id)
        {
            OrderEntity? order = await _context.Orders.FindAsync(id);
            if (order?.IsDeleted == false)
            {
                order.IsDeleted = true;
                _context.Update(order);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IReadOnlyCollection<OrderEntity>> GetAllByAsync(OrderParameters parameters)
        {

            IQueryable<OrderEntity>? query = _context.Orders
                .Where(
                    order =>
                        !order.IsDeleted
                        && order.UserId == parameters.UserId
                );
            if (parameters.IsPast)
            {
                query = query.Where(
                    order => order.DateTime < DateTime.Now
                );
            }
            else
            {
                query = query.Where(
                    order => order.DateTime >= DateTime.Now
                );
            }
            List<OrderEntity> orders = await query.ToListAsync();
            return orders;
        }

        public ValueTask<OrderEntity?> GetByAsync(int id)
        {
            // This measure is temporary. The directive will be removed with the release of EF 6.0
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return _context.Orders.FindAsync(id);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public Task<OrderEntity?> GetByAsync(int sessionId, int seatId)
        {
            // This measure is temporary. The directive will be removed with the release of EF 6.0
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return _context.Orders
                .Where(
                    order =>
                        order.SessionId == sessionId
                        && _context.SeatOrders
                            .Where(seatOrder => seatOrder.OrderId == order.Id)
                            .Any()
                )
                .FirstOrDefaultAsync();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public Task UpdateAsync(OrderEntity order)
        {
            _context.Orders.Update(order);
            return _context.SaveChangesAsync();
        }
    }
}
