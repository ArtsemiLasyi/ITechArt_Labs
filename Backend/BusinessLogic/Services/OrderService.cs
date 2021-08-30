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

        public OrderService(OrderRepository orderRepository, SeatTypePriceService seatTypePriceService)
        {
            _orderRepository = orderRepository;
            _seatTypePriceService = seatTypePriceService;
        }

        public Task CreateAsync(OrderModel order)
        {
            OrderEntity? orderEntity = order.Adapt<OrderEntity>();
            orderEntity.DateTime = System.DateTime.Now;
            return _orderRepository.CreateAsync(orderEntity);
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
            PriceModel servicesPrice = await GetServicesSum(model.SessionId, model.Services);
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

        private PriceModel GetServicesSum(int sessionId, IReadOnlyCollection<ServiceModel> services)
        {
            decimal value = 0;
            string currency = string.Empty;
            foreach (ServiceModel service in services)
            {
                CinemaServiceModel? cinemaSericeModel = GetCinemaIdBySessionId(sessionId);
                if (cinemaSericeModel == null)
                {
                    continue;
                }
                currency = cinemaSericeModel.Price.Currency;
                value += cinemaSericeModel.Price.Value;
            }
            return new PriceModel(value, currency);
        }

        private CinemaServiceModel GetCinemaIdBySessionId(int sessionId)
        {
            // Todo
            throw new NotImplementedException();
        }
    }
}
