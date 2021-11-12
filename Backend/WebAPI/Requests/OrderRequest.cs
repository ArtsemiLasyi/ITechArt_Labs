using System;
using System.Collections.Generic;

namespace WebAPI.Requests
{
    public class OrderRequest
    {
        public int SessionId { get; set; }
        public DateTime RegistratedAt { get; set; }
        public SessionSeatsRequest SessionSeats { get; set; } = null!;
        public IReadOnlyCollection<CinemaServiceRequest> CinemaServices { get; set; } = null!; 
    }
}
