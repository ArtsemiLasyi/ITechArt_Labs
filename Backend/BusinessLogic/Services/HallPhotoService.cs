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
    public class HallPhotoService
    {
        private readonly HallPhotoFileStorage _hallPhotoFileStorage;
        private readonly HallPhotoRepository _hallPhotoRepository;

        public HallPhotoService(
            HallPhotoFileStorage hallPhotoFileStorage,
            HallPhotoRepository hallPhotoRepository)
        {
            _hallPhotoFileStorage = hallPhotoFileStorage;
            _hallPhotoRepository = hallPhotoRepository;
        }

        public async Task UploadAsync(int id, Stream stream, string extension)
        {
            string fileName = GetFileName(extension);
            await _hallPhotoFileStorage.CreateAsync(stream, fileName);

            HallPhotoEntity? entity = await _hallPhotoRepository.GetByAsync(id);
            if (entity == null)
            {
                entity = new HallPhotoEntity()
                {
                    HallId = id,
                    FileName = fileName
                };
                await _hallPhotoRepository.CreateAsync(entity);
                return;
            }
            _hallPhotoFileStorage.Delete(entity.FileName);
            entity.FileName = fileName;
            await _hallPhotoRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            HallPhotoEntity? entity = await _hallPhotoRepository.GetByAsync(id);
            if (entity == null)
            {
                return false;
            }
            return _hallPhotoFileStorage.Delete(entity.FileName);
        }

        public async Task<PosterModel?> GetAsync(int id)
        {
            HallPhotoEntity? entity = await _hallPhotoRepository.GetByAsync(id);
            if (entity == null)
            {
                return null;
            }

            Stream? stream = _hallPhotoFileStorage.Get(entity.FileName);
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
