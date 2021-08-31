using System;

namespace BusinessLogic.Models
{
    public class SessionSeatModel
    {
        public int SeatId { get; set; }
        public int SessionId { get; set; }
        public int? UserId { get; set; }
        public SessionSeatStatus Status { get; set; }
        public DateTime DateTime { get; set; }
    }
}
