namespace BusinessLogic.Models
{
    public class SeatOrderModel
    {
        public int SeatId { get; set; }
        public int OrderId { get; set; }
        public SeatModel Seat { get; set; } = null!;
    }
}
