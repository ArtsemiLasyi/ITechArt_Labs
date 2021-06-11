using DataAccess.Storages;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class FileService
    {
        private readonly FileStorage _fileStorage;

        public FileService(FileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public async Task<string> CreateAsync(Stream stream, string path, string extension)
        {
            Guid guid = Guid.NewGuid();
            string filename = guid.ToString() + extension;
            await _fileStorage.CreateAsync(stream, path, filename);
            return filename;
        }

        public Stream Get(string path, string filename)
        {
            return _fileStorage.Get(path, filename);
        }
    }
}
