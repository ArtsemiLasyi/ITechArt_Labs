using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class SeatEntity
    {
        public int Id { get; set; }
        public int HallId { get; set; }
        public int SeatTypeId { get; set; }
        public int Row { get; set; }
        public int Place { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("HallId")]
        public HallEntity Hall { get; set; } = null!;
        public SeatTypeEntity SeatType { get; set; } = null!;
    }
}
