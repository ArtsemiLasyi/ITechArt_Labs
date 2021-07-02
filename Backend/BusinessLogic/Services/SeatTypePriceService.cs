using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class SeatTypePriceService
    {
        private readonly SeatTypePriceRepository _seatTypePriceRepository;

        public SeatTypePriceService(SeatTypePriceRepository seatTypePriceRepository)
        {
            _seatTypePriceRepository = seatTypePriceRepository;
        }

        public async Task<SeatTypePriceModel> CreateAsync(SeatTypePriceModel model)
        {
            SeatTypePriceEntity entity = model.Adapt<SeatTypePriceEntity>();
            await _seatTypePriceRepository.CreateAsync(entity);
            return entity.Adapt<SeatTypePriceModel>();
        }

        public async Task<SeatTypePriceModel?> GetByAsync(int sessionId, int seatTypeId)
        {
            SeatTypePriceEntity? entity = await _seatTypePriceRepository.GetByAsync(sessionId, seatTypeId);
            if (entity == null)
            {
                return null;
            }
            SeatTypePriceModel model = entity.Adapt<SeatTypePriceModel>();
            model.Price = new PriceModel(entity.Price, entity.Currency.Name);
            return model;
        }

        public async Task<IReadOnlyCollection<SeatTypePriceModel>> GetAllByAsync(int cinemaId)
        {
            IReadOnlyCollection<SeatTypePriceEntity> models = await _seatTypePriceRepository.GetAllByAsync(cinemaId);
            return models.Adapt<IReadOnlyCollection<SeatTypePriceModel>>();
        }

        public Task EditAsync(SeatTypePriceModel model)
        {
            SeatTypePriceEntity entity = model.Adapt<SeatTypePriceEntity>();
            return _seatTypePriceRepository.UpdateAsync(entity);
        }
    }
}
