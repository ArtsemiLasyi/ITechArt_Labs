using DataAccess.Options;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;

namespace DataAccess.Storages
{
    public class PosterFileStorage
    {
        private readonly StorageOptions _storageSnapshotOptions;

        public PosterFileStorage(IOptionsSnapshot<StorageOptions> storageSnapshotOptionsAccessor)
        {
            _storageSnapshotOptions = storageSnapshotOptionsAccessor.Value;
        }

        public Task CreateAsync(Stream stream, string fileName)
        {
            string path = GetPath();

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fullPath = Path.Combine(path, fileName);
            using FileStream fileStream = new FileStream(fullPath, FileMode.Create);
            return stream.CopyToAsync(fileStream);
        }

        public bool Delete(string fileName)
        {
            string path = GetPath();
            string fullPath = Path.Combine(path, fileName);

            bool fileExists = File.Exists(fullPath);
            if (fileExists)
            {
                File.Delete(fullPath);
                return true;
            }
            return false;
        }

        public Stream? Get(string fileName)
        {
            string path = GetPath();
            string fullPath = Path.Combine(path, fileName);

            bool fileExists = File.Exists(fullPath);
            if (!fileExists)
            {
                return new FileStream(fullPath, FileMode.Open);
            }
            return null;
        }

        private string GetPath()
        {
            return Path.Combine(_storageSnapshotOptions.Path, _storageSnapshotOptions.Films);
        }
    }
}
