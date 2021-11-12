using System;

namespace DataAccess.Parameters
{
    public class SessionEntitySearchParameters
    {
        public int? FilmId { get; set; }
        public DateTime? FirstSessionDateTime { get; set; }
        public DateTime? LastSessionDateTime { get; set; }
        public int? FreeSeatsNumber { get; set; }
    }
}
