using System.IO;

namespace BusinessLogic.Models
{
    public class PosterModel
    {
        public int FilmId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public Stream FileStream { get; set; } = null!;
    }
}
