using WebAPI.Models;

namespace WebAPI.Responses
{
    public class SessionSeatResponse
    {
        public int SeatId { get; set; }
        public int SessionId { get; set; }
        public SessionSeatStatus Status { get; set; }
    }
}
