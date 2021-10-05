using System;

namespace BusinessLogic.Models
{
    public class SessionSeatModel
    {
        public int SeatId { get; set; }
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public SessionSeatStatus Status { get; set; }
        public DateTime TakenAt { get; set; }
        public int Row { get; set; }
        public int Place { get; set; }
        public int SeatTypeId { get; set; }
    }
}
