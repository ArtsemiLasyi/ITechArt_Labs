using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class CinemaPhotoEntity
    {
        public int CinemaId { get; set; }
        public string FileName { get; set; } = null!;
    }
}
