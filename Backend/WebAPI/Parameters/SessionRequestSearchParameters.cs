using System;

namespace WebAPI.Parameters
{
    public class SessionRequestSearchParameters
    {
        public int? FilmId { get; set; }
        public DateTime? FirstSessionDateTime { get; set; }
        public DateTime? LastSessionDateTime { get; set; }
        public int? FreeSeatsNumber { get; set; }
    }
}
