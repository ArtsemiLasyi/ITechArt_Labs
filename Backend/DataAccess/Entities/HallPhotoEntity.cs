using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class HallPhotoEntity
    {
        [Key]
        public int HallId { get; set; }
        public string FileName { get; set; } = null!;
    }
}
