using BusinessLogic.Models;
using BusinessLogic.Parameters;
using DataAccess.Entities;
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
        private readonly CurrencyService _currencyService;
        private readonly SeatOrderService _seatOrderService;
        private readonly SessionSeatService _sessionSeatService;

        public OrderService(
            OrderRepository orderRepository,
            SeatTypePriceService seatTypePriceService,
            CurrencyService сurrencyService,
            SeatOrderService seatOrderService,
            SessionSeatService sessionSeatService)
        {
            _orderRepository = orderRepository;
            _seatTypePriceService = seatTypePriceService;
            _currencyService = сurrencyService;
            _seatOrderService = seatOrderService;
            _sessionSeatService = sessionSeatService;
        }

        public async Task CreateAsync(OrderModel order)
        {
            OrderEntity? orderEntity = order.Adapt<OrderEntity>();
            orderEntity.DateTime = DateTime.UtcNow;

            PriceModel price = await GetSum(order);
            orderEntity.Price = price.Value;

            CurrencyModel currencyModel = await _currencyService.GetByAsync(price.Currency);
            orderEntity.CurrencyId = currencyModel.Id;

            int id = await _orderRepository.CreateAsync(orderEntity);
            await _seatOrderService.CreateAsync(id, order.Seats);
            await _sessionSeatService.OrderAsync(orderEntity.SessionId, order.Seats);
        }

        public async Task<IReadOnlyCollection<OrderModel>> GetAllByAsync(OrderParameters parameters)
        {
            IReadOnlyCollection<OrderEntity> models = await _orderRepository
                .GetAllByAsync(
                    parameters.Adapt<DataAccess.Parameters.OrderParameters>()
                );
            return models.Adapt<IReadOnlyCollection<OrderModel>>();
        }

        public async Task<PriceModel> GetSum(OrderModel model)
        {
            decimal value = 0;
            string currency = string.Empty;
            PriceModel seatsPrice = await GetSeatsSum(model.SessionId, model.Seats);
            PriceModel servicesPrice = GetServicesSum(model.SessionId, model.CinemaServices);
            value += seatsPrice.Value;
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

        private async Task<PriceModel> GetSeatsSum(int sessionId, SeatsModel seatsModel)
        {
            decimal value = 0;
            string currency = string.Empty;
            foreach (SeatModel seat in seatsModel.Seats)
            {
                SeatTypePriceModel? seatTypePriceModel = await _seatTypePriceService
                   .GetByAsync(sessionId, seat.SeatTypeId);
                if (seatTypePriceModel == null)
                {
                    continue;
                }
                currency = seatTypePriceModel.Price.Currency;
                value += seatTypePriceModel.Price.Value;
            }
            return new PriceModel(value, currency);
        }

        private PriceModel GetServicesSum(int sessionId, IReadOnlyCollection<CinemaServiceModel> services)
        {
            decimal value = 0;
            string currency = string.Empty;
            foreach (CinemaServiceModel service in services)
            {
                currency = service.Price.Currency;
                value += service.Price.Value;
            }
            return new PriceModel(value, currency);
        }
    }
}
