using System.IO;
using System.Threading.Tasks;

namespace DataAccess.Storages
{
    public class FileStorage
    {
        public async Task CreateAsync(Stream stream, string path, string filename)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fullPath = Path.Combine(path, filename);
            using FileStream? fileStream = new FileStream(fullPath, FileMode.Create);
            await stream.CopyToAsync(fileStream);
        }

        public Stream Get(string path, string filename)
        {
            string fullPath = Path.Combine(path, filename);
            return new FileStream(fullPath, FileMode.Open);
        }
    }
}
