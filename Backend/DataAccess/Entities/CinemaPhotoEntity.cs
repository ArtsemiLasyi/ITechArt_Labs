using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class CinemaPhotoEntity
    {
        [Key]
        public int CinemaId { get; set; }
        public string FileName { get; set; } = null!;
    }
}
