using System;

namespace DataAccess.Parameters
{
    public class SessionEntitySearchParameters
    {
        public DateTime? FirstSessionDateTime { get; set; }
        public DateTime? LastSessionDateTime { get; set; }
        public int? FreeSeatsNumber { get; set; }
    }
}
