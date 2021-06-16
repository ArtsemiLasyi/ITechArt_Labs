using DataAccess.Storages;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class PosterFileService
    {
        private readonly PosterFileStorage _posterFileStorage;

        public PosterFileService(PosterFileStorage fileStorage)
        {
            _posterFileStorage = fileStorage;
        }

        public async Task<string> CreateAsync(Stream stream, string extension)
        {
            Guid guid = Guid.NewGuid();
            string filename = guid.ToString() + extension;
            await _posterFileStorage.CreateAsync(stream, filename);
            return filename;
        }

        public bool Delete(string fileName)
        {
            return _posterFileStorage.Delete(fileName);
        }

        public Stream? Get(string fileName)
        {
            return _posterFileStorage.Get(fileName);
        }
    }
}
