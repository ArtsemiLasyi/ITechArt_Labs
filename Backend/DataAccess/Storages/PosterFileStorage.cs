using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;

namespace DataAccess.Storages
{
    public class PosterFileStorage
    {
        private readonly Options.FileOptions _fileSnapshotOptions;

        public PosterFileStorage(IOptionsSnapshot<Options.FileOptions> fileSnapshotOptionsAccessor)
        {
            _fileSnapshotOptions = fileSnapshotOptionsAccessor.Value;
        }

        public Task CreateAsync(Stream stream, string filename)
        {
            string path = GetPath();

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fullPath = Path.Combine(path, filename);
            using FileStream fileStream = new FileStream(fullPath, FileMode.Create);
            return stream.CopyToAsync(fileStream);
        }

        public bool Delete(string filename)
        {
            string path = GetPath();
            string fullPath = Path.Combine(path, filename);

            bool fileExists = File.Exists(fullPath);
            if (!fileExists)
            {
                return false;
            }
            else
            {
                File.Delete(fullPath);
                return true;
            }
        }

        public Stream? Get(string filename)
        {
            string path = GetPath();
            string fullPath = Path.Combine(path, filename);

            bool fileExists = File.Exists(fullPath);
            if (!fileExists)
            {
                return null;
            }
            else
            {
                return new FileStream(fullPath, FileMode.Open);
            } 
        }

        private string GetPath()
        {
            return Path.Combine(_fileSnapshotOptions.Path, _fileSnapshotOptions.Films);
        }
    }
}
