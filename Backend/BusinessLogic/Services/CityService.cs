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
    public class CityService
    {
        private readonly CityRepository _cityRepository;

        public CityService(CityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<int> CreateAsync(CityModel city)
        {
            CityEntity? cityEntity = city.Adapt<CityEntity>();
            await _cityRepository.CreateAsync(cityEntity);
            return cityEntity.Id;
        }

        public async Task<IReadOnlyCollection<CityModel>> GetAsync(CityModelSearchParameters parameters)
        {
            CityEntitySearchParameters searchParameters = parameters.Adapt<CityEntitySearchParameters>(); 
            IReadOnlyCollection<CityEntity> models = await _cityRepository.GetAsync(searchParameters);
            return models.Adapt<IReadOnlyCollection<CityModel>>();
        }

        public async Task<CityModel?> GetByAsync(int id)
        {
            CityEntity? city = await _cityRepository.GetByAsync(id);
            return city?.Adapt<CityModel>();
        }

        public async Task<CityModel?> GetByAsync(string name)
        {
            CityEntity? city = await _cityRepository.GetByAsync(name);
            return city?.Adapt<CityModel>();
        }
    }
}
