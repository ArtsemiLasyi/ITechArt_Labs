using WebAPI.Models;

namespace WebAPI.Responses
{
    public class SessionSeatResponse
    {
        public int UserId { get; set; }
        public int SeatId { get; set; }
        public int SessionId { get; set; }
        public SessionSeatStatus Status { get; set; }
        public int Row { get; set; }
        public int Place { get; set; }
        public int SeatTypeId { get; set; }
    }
}
