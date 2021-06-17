using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.Storages;
using Mapster;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class PosterService
    {
        private readonly PosterFileStorage _posterFileStorage;
        private readonly PosterRepository _posterRepository;

        public PosterService(PosterFileStorage fileStorage, PosterRepository posterRepository)
        {
            _posterFileStorage = fileStorage;
            _posterRepository = posterRepository;
        }

        public async Task UploadAsync(int id, Stream stream, string extension)
        {
            string fileName = GetFileName(extension);
            await _posterFileStorage.CreateAsync(stream, fileName);

            PosterEntity? entity = await _posterRepository.GetByAsync(id);
            if (entity == null)
            {
                entity = new PosterEntity()
                {
                    FilmId = id,
                    FileName = fileName
                };
                await _posterRepository.CreateAsync(entity);
            }
            _posterFileStorage.Delete(entity.FileName);
            entity.FileName = fileName;
            await _posterRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            PosterEntity? entity = await _posterRepository.GetByAsync(id);
            if (entity == null)
            {
                return false;
            }
            return _posterFileStorage.Delete(entity.FileName);
        }

        public async Task<PosterModel?> GetAsync(int id)
        {
            PosterEntity? entity = await _posterRepository.GetByAsync(id);
            if (entity == null)
            {
                return null;
            }

            Stream? stream = _posterFileStorage.Get(entity.FileName);
            if (stream == null)
            {
                return null;
            }

            PosterModel model = entity.Adapt<PosterModel>();
            model.FileStream = stream;
            return model;
        }

        private string GetFileName(string extension)
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString() + extension;
        }
    }
}
