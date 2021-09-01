using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class CinemaServiceService
    {
        private readonly CinemaServiceRepository _cinemaServiceRepository;

        public CinemaServiceService(CinemaServiceRepository cinemaServiceRepository)
        {
            _cinemaServiceRepository = cinemaServiceRepository;
        }

        public async Task<CinemaServiceModel> CreateAsync(CinemaServiceModel model)
        {
            CinemaServiceEntity entity = model.Adapt<CinemaServiceEntity>();
            await _cinemaServiceRepository.CreateAsync(entity);
            return entity.Adapt<CinemaServiceModel>();
        }

        public async Task<CinemaServiceModel?> GetByAsync(int serviceId, int cinemaId) 
        {
            CinemaServiceEntity? entity = await _cinemaServiceRepository.GetByAsync(serviceId, cinemaId);
            if (entity == null)
            {
                return null;
            }
            CinemaServiceModel model = entity.Adapt<CinemaServiceModel>();
            model.Price = new PriceModel(entity.Price, entity.Currency.Adapt<CurrencyModel>());
            return model;
        }

        public async Task<IReadOnlyCollection<CinemaServiceModel>> GetAllByAsync(int cinemaId)
        {
            IReadOnlyCollection<CinemaServiceEntity> models = await _cinemaServiceRepository.GetAllByAsync(cinemaId);
            return models.Adapt<IReadOnlyCollection<CinemaServiceModel>>();
        }

        public Task EditAsync(CinemaServiceModel model)
        {
            CinemaServiceEntity entity = model.Adapt<CinemaServiceEntity>();
            return _cinemaServiceRepository.UpdateAsync(entity);
        }
    }
}
