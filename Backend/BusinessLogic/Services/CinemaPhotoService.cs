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
    public class CinemaPhotoService
    {
        private readonly CinemaPhotoFileStorage _cinemaPhotoFileStorage;
        private readonly CinemaPhotoRepository _cinemaPhotoRepository;

        public CinemaPhotoService(
            CinemaPhotoFileStorage cinemaPhotoFileStorage, 
            CinemaPhotoRepository cinemaPhotoRepository)
        {
            _cinemaPhotoFileStorage = cinemaPhotoFileStorage;
            _cinemaPhotoRepository = cinemaPhotoRepository;
        }

        public async Task UploadAsync(int id, Stream stream, string extension)
        {
            string fileName = GetFileName(extension);
            await _cinemaPhotoFileStorage.CreateAsync(stream, fileName);

            CinemaPhotoEntity? entity = await _cinemaPhotoRepository.GetByAsync(id);
            if (entity == null)
            {
                entity = new CinemaPhotoEntity()
                {
                    CinemaId = id,
                    FileName = fileName
                };
                await _cinemaPhotoRepository.CreateAsync(entity);
                return;
            }
            _cinemaPhotoFileStorage.Delete(entity.FileName);
            entity.FileName = fileName;
            await _cinemaPhotoRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            CinemaPhotoEntity? entity = await _cinemaPhotoRepository.GetByAsync(id);
            if (entity == null)
            {
                return false;
            }
            return _cinemaPhotoFileStorage.Delete(entity.FileName);
        }

        public async Task<PosterModel?> GetAsync(int id)
        {
            CinemaPhotoEntity? entity = await _cinemaPhotoRepository.GetByAsync(id);
            if (entity == null)
            {
                return null;
            }

            Stream? stream = _cinemaPhotoFileStorage.Get(entity.FileName);
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
