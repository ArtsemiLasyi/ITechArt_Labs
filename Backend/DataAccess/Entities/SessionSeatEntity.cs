﻿namespace DataAccess.Entities
{
    public class SessionSeatEntity
    {
        public int SeatId { get; set; }
        public int SessionId { get; set; }
        public bool IsTaken { get; set; }
    }
}
