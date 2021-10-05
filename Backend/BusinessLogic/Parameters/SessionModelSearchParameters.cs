using System;

namespace BusinessLogic.Parameters
{
    public class SessionModelSearchParameters
    {
        public DateTime? FirstSessionDateTime { get; set; }
        public DateTime? LastSessionDateTime { get; set; }
        public int? FreeSeatsNumber { get; set; }
    }
}
