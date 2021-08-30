using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class SeatOrderEntity
    {
        public int SeatId { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("SeatId")]
        public SeatEntity Seat { get; set; } = null!;
    }
}
