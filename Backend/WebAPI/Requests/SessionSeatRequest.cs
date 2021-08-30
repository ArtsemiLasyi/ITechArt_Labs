namespace WebAPI.Requests
{
    public class SessionSeatRequest
    {
        public int SeatId { get; set; }
        public int SessionId { get; set; }
        public bool IsTaken { get; set; }
    }
}
