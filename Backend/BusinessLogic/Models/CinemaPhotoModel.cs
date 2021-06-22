using System.IO;

namespace BusinessLogic.Models
{
    public class CinemaPhotoModel
    {
        public int CinemaId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public Stream FileStream { get; set; } = null!;
    }
}
