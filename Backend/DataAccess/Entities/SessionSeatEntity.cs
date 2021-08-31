using System;

namespace DataAccess.Entities
{
    public class SessionSeatEntity
    {
        public int SeatId { get; set; }
        public int SessionId { get; set; }
        public int? UserId { get; set; }
        public int Status { get; set; }
        public DateTime DateTime { get; set; }
    }
}
