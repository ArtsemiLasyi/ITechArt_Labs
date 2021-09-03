using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class SeatOrderService
    {
        private readonly SeatOrderRepository _cinemaServiceRepository;

        public SeatOrderService(SeatOrderRepository cinemaServiceRepository)
        {
            _cinemaServiceRepository = cinemaServiceRepository;
        }

        public async Task<SeatOrderModel> CreateAsync(SeatOrderModel model)
        {
            SeatOrderEntity entity = model.Adapt<SeatOrderEntity>();
            await _cinemaServiceRepository.CreateAsync(entity);
            return entity.Adapt<SeatOrderModel>();
        }

        public async Task CreateAsync(int orderId, SessionSeatsModel sessionSeats)
        {
            foreach(SessionSeatModel sessionSeat in sessionSeats.Value)
            {
                await CreateAsync(
                    new SeatOrderModel()
                    {
                        OrderId = orderId,
                        SeatId = sessionSeat.SeatId
                    }
                );
            }
        }

        public async Task<SeatOrderModel?> GetByAsync(int orderId, int seatId)
        {
            SeatOrderEntity? entity = await _cinemaServiceRepository.GetByAsync(orderId, seatId);
            if (entity == null)
            {
                return null;
            }
            SeatOrderModel model = entity.Adapt<SeatOrderModel>();
            return model;
        }

        public async Task<IReadOnlyCollection<SeatOrderModel>> GetAllByAsync(int orderId)
        {
            IReadOnlyCollection<SeatOrderEntity> models = await _cinemaServiceRepository.GetAllByAsync(orderId);
            return models.Adapt<IReadOnlyCollection<SeatOrderModel>>();
        }
    }
}
