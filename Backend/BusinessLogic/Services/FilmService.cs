﻿using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class FilmService
    {
        private readonly FilmRepository _filmRepository;

        public FilmService(FilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public async Task<int> CreateAsync(FilmModel film)
        {
            FilmEntity? filmEntity = film.Adapt<FilmEntity>();
            filmEntity.DurationInTicks = film.Duration.Ticks;
            await _filmRepository.CreateAsync(filmEntity);
            return filmEntity.Id;
        }
        
        public async Task<IReadOnlyCollection<FilmModel>> GetAsync(int pageNumber, int pageSize)
        {
            IReadOnlyCollection<FilmEntity> models = await _filmRepository.GetAsync(pageNumber, pageSize);
            return models.Adapt<IReadOnlyCollection<FilmModel>>();
        }

        public async Task<FilmModel?> GetByAsync(int id)
        {
            FilmEntity? user = await _filmRepository.GetByAsync(id);
            return user?.Adapt<FilmModel>();
        }

        public Task<bool> DeleteByAsync(int id)
        {
            return _filmRepository.DeleteByAsync(id);
        }

        public Task EditAsync(FilmModel film)
        {
            FilmEntity filmEntity = film.Adapt<FilmEntity>();
            filmEntity.DurationInTicks = film.Duration.Ticks;
            return _filmRepository.UpdateAsync(filmEntity);
        }
    }
}