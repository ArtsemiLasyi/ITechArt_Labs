using System;

namespace BusinessLogic.Parameters
{
    public class SessionModelSearchParameters
    {
        public int? FilmId { get; set; }
        public DateTime? FirstSessionDateTime { get; set; }
        public DateTime? LastSessionDateTime { get; set; }
        public int? FreeSeatsNumber { get; set; }
    }
}
