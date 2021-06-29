using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class HallPhotoEntity
    {
        public int HallId { get; set; }
        public string FileName { get; set; } = null!;
    }
}
