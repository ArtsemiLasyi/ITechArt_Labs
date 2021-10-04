using BusinessLogic.Models;
using BusinessLogic.Parameters;
using DataAccess.Entities;
using DataAccess.Parameters;
using DataAccess.Repositories;
using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class CinemaService
    {
        private readonly CinemaRepository _cinemaRepository;
        private readonly CityService _cityService;

        public CinemaService(
            CinemaRepository cinemaRepository,
            CityService cityService)
        {
            _cinemaRepository = cinemaRepository;
            _cityService = cityService;
        }

        public async Task<int> CreateAsync(CinemaModel cinema)
        {
            int cityId = await GetCityIdAsync(cinema.CityName);
            CinemaEntity? cinemaEntity = cinema.Adapt<CinemaEntity>();
            cinemaEntity.CityId = cityId;
            await _cinemaRepository.CreateAsync(cinemaEntity);
            return cinemaEntity.Id;
        }

        public async Task<IReadOnlyCollection<CinemaModel>> GetAllByAsync(int cityId, CinemaModelSearchParameters parameters)
        {
            CinemaEntitySearchParameters searchParameters = parameters.Adapt<CinemaEntitySearchParameters>();
            IReadOnlyCollection<CinemaEntity> models = await _cinemaRepository.GetAllByAsync(cityId, searchParameters);
            return models.Adapt<IReadOnlyCollection<CinemaModel>>();
        }

        public async Task<CinemaModel?> GetByAsync(int id)
        {
            CinemaEntity? cinemaEntity = await _cinemaRepository.GetByAsync(id);
            return cinemaEntity?.Adapt<CinemaModel>();
        }

        public Task<bool> DeleteByAsync(int id)
        {
            return _cinemaRepository.DeleteByAsync(id);
        }

        public async Task EditAsync(CinemaModel cinema)
        {
            int cityId = await GetCityIdAsync(cinema.CityName);
            CinemaEntity? cinemaEntity = cinema.Adapt<CinemaEntity>();
            cinemaEntity.CityId = cityId;
            await _cinemaRepository.UpdateAsync(cinemaEntity);
        }

        private async Task<int> GetCityIdAsync(string name)
        {
            int cityId;
            CityModel? cityModel = await _cityService.GetByAsync(name);
            if (cityModel == null)
            {
                cityId = await _cityService.CreateAsync(
                    new CityModel()
                    {
                        Name = name
                    }
                );
            }
            else
            {
                cityId = cityModel.Id;
            }
            return cityId;
        }
    }
}
