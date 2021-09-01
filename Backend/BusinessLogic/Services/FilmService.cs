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
            await _filmRepository.CreateAsync(filmEntity);
            return filmEntity.Id;
        }
        
        public async Task<IReadOnlyCollection<FilmModel>> GetAsync(int pageNumber, int pageSize, FilmModelSearchParameters parameters)
        {
            FilmEntitySearchParameters filmParameters = parameters.Adapt<FilmEntitySearchParameters>();
            IReadOnlyCollection<FilmEntity> models = await _filmRepository.GetAsync(pageNumber, pageSize, filmParameters);
            return models.Adapt<IReadOnlyCollection<FilmModel>>();
        }

        public async Task<FilmModel?> GetByAsync(int id)
        {
            FilmEntity? film = await _filmRepository.GetByAsync(id);
            return film?.Adapt<FilmModel>();
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
