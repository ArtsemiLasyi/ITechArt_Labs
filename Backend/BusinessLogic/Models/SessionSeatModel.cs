namespace BusinessLogic.Models
{
    public class SessionSeatModel
    {
        public int SeatId { get; set; }
        public int SessionId { get; set; }
        public bool IsTaken { get; set; }
    }
}
