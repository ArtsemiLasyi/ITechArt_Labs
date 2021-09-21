using BusinessLogic.Models;
using BusinessLogic.Parameters;
using DataAccess.Entities;
using DataAccess.Parameters;
using DataAccess.Repositories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly SeatTypePriceService _seatTypePriceService;
        private readonly SeatOrderService _seatOrderService;
        private readonly SessionSeatService _sessionSeatService;
        private readonly SeatService _seatService;

        public OrderService(
            OrderRepository orderRepository,
            SeatTypePriceService seatTypePriceService,
            SeatOrderService seatOrderService,
            SessionSeatService sessionSeatService,
            SeatService seatService)
        {
            _orderRepository = orderRepository;
            _seatTypePriceService = seatTypePriceService;
            _seatOrderService = seatOrderService;
            _sessionSeatService = sessionSeatService;
            _seatService = seatService;
        }

        public async Task CreateAsync(OrderModel order)
        {
            OrderEntity orderEntity = order.Adapt<OrderEntity>();
            orderEntity.RegistratedAt = DateTime.UtcNow;

            PriceModel price = await GetSumAsync(order);
            orderEntity.Price = price.Value;
            orderEntity.CurrencyId = price.Currency.Id;

            int id = await _orderRepository.CreateAsync(orderEntity);
            await _seatOrderService.CreateAsync(id, order.SessionSeats);
            await _sessionSeatService.OrderAsync(order.SessionSeats);
        }

        public async Task<IReadOnlyCollection<OrderModel>> GetAllByAsync(int userId, OrderModelSearchParameters parameters)
        {
            IReadOnlyCollection<OrderEntity> models = await _orderRepository
                .GetAllByAsync(
                    userId,
                    parameters.Adapt<OrderEntitySearchParameters>()
                );
            return models.Adapt<IReadOnlyCollection<OrderModel>>();
        }

        public async Task<PriceModel> GetSumAsync(OrderModel model)
        {
            decimal value = 0;
            CurrencyModel currency;
            PriceModel seatsPrice = await GetSeatsSumAsync(model.SessionId, model.SessionSeats);
            PriceModel servicesPrice = GetServicesSum(model.SessionId, model.CinemaServices);
            value = seatsPrice.Value + servicesPrice.Value;
            currency = seatsPrice.Currency;
            PriceModel price = new PriceModel(value, currency);
            return price;
        }

        public async Task<OrderModel?> GetByAsync(int id)
        {
            OrderEntity? orderEntity = await _orderRepository.GetByAsync(id);
            return orderEntity?.Adapt<OrderModel>();
        }

        public async Task<OrderModel?> GetByAsync(int sessionId, int seatId)
        {
            OrderEntity? orderEntity = await _orderRepository.GetByAsync(sessionId, seatId);
            return orderEntity?.Adapt<OrderModel>();
        }

        public async Task EditAsync(OrderModel order)
        {
            OrderEntity orderEntity = order.Adapt<OrderEntity>();
            await _orderRepository.UpdateAsync(orderEntity);
        }

        public Task<bool> DeleteByAsync(int id)
        {
            return _orderRepository.DeleteByAsync(id);
        }

        private async Task<PriceModel> GetSeatsSumAsync(int sessionId, SessionSeatsModel sessionSeatsModel)
        {
            decimal value = 0;
            CurrencyModel currency = new CurrencyModel();
            foreach (SessionSeatModel sessionSeat in sessionSeatsModel.Value)
            {
                SeatModel? model = await _seatService
                    .GetByAsync(sessionSeat.SeatId);
                if (model == null)
                {
                    throw new Exception("Seat does not exist!");
                }

                SeatTypePriceModel? seatTypePriceModel = await _seatTypePriceService
                   .GetByAsync(sessionId, model.SeatTypeId);
                if (seatTypePriceModel == null)
                {
                    throw new Exception("Price for type of seats does not exist!");
                }

                currency = seatTypePriceModel.Price.Currency;
                value += seatTypePriceModel.Price.Value;
            }
            return new PriceModel(value, currency);
        }

        private PriceModel GetServicesSum(int sessionId, IReadOnlyCollection<CinemaServiceModel> services)
        {
            decimal value = 0;
            CurrencyModel currency = new CurrencyModel();
            foreach (CinemaServiceModel service in services)
            {
                currency = service.Price.Currency;
                value += service.Price.Value;
            }
            return new PriceModel(value, currency);
        }
    }
}
