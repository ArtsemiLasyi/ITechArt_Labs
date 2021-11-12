using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using Mapster;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class SeatOrderService
    {
        private readonly SeatOrderRepository _seatOrderRepository;

        public SeatOrderService(SeatOrderRepository cinemaServiceRepository)
        {
            _seatOrderRepository = cinemaServiceRepository;
        }

        public async Task<SeatOrderModel> CreateAsync(SeatOrderModel model)
        {
            SeatOrderEntity entity = model.Adapt<SeatOrderEntity>();
            await _seatOrderRepository.CreateAsync(entity);
            return entity.Adapt<SeatOrderModel>();
        }

        public Task CreateAsync(int orderId, SessionSeatsModel sessionSeats)
        {
            IReadOnlyCollection<SeatOrderEntity> seatOrders = sessionSeats.Value
                .Select(
                    sessionSeat =>
                    {
                        return new SeatOrderModel()
                        {
                            OrderId = orderId,
                            SeatId = sessionSeat.SeatId
                        };
                    }
                )
                .ToList()
                .AsReadOnly()
                .Adapt<IReadOnlyCollection<SeatOrderEntity>>();
            return _seatOrderRepository.CreateAsync(seatOrders);
        }

        public async Task<SeatOrderModel?> GetByAsync(int orderId, int seatId)
        {
            SeatOrderEntity? entity = await _seatOrderRepository.GetByAsync(orderId, seatId);
            if (entity == null)
            {
                return null;
            }
            SeatOrderModel model = entity.Adapt<SeatOrderModel>();
            return model;
        }

        public async Task<IReadOnlyCollection<SeatOrderModel>> GetAllByAsync(int orderId)
        {
            IReadOnlyCollection<SeatOrderEntity> models = await _seatOrderRepository.GetAllByAsync(orderId);
            return models.Adapt<IReadOnlyCollection<SeatOrderModel>>();
        }
    }
}
