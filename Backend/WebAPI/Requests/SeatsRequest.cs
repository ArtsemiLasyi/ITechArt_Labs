using System.Collections.Generic;

namespace WebAPI.Requests
{
    public class SeatsRequest
    {
        public IReadOnlyCollection<SeatRequest> Seats { get; set; } = null!;
    }
}
