﻿using BusinessLogic.Models;
using DataAccess.Entities;
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
            CityModel? cityModel = await _cityService.GetByAsync(cinema.CityName);
            int cityId;
            if (cityModel == null)
            {
                cityId = await _cityService.CreateAsync(new CityModel()
                {
                    Name = cinema.Name
                });
            }
            else
            {
                cityId = cityModel.Id;
            }

            CinemaEntity? cinemaEntity = cinema.Adapt<CinemaEntity>();
            cinemaEntity.CityId = cityId;
            await _cinemaRepository.CreateAsync(cinemaEntity);
            return cinemaEntity.Id;
        }

        public async Task<IReadOnlyCollection<CinemaModel>> GetAllByAsync(int cityId)
        {
            IReadOnlyCollection<CinemaEntity> models = await _cinemaRepository.GetAllByAsync(cityId);
            List<CinemaModel> result = new List<CinemaEntity>(models).Adapt<List<CinemaModel>>();
            return result;
        }

        public async Task<CinemaModel?> GetByAsync(int id)
        {
            CinemaEntity? user = await _cinemaRepository.GetByAsync(id);
            return user?.Adapt<CinemaModel>();
        }

        public Task<bool> DeleteByAsync(int id)
        {
            return _cinemaRepository.DeleteByAsync(id);
        }

        public Task EditAsync(CinemaModel cinema)
        {
            CinemaEntity cinemaEntity = cinema.Adapt<CinemaEntity>();
            return _cinemaRepository.UpdateAsync(cinemaEntity);
        }
    }
}
