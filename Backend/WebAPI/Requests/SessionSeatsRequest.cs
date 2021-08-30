using System.Collections.Generic;

namespace WebAPI.Requests
{
    public class SessionSeatsRequest
    {
        public IReadOnlyCollection<SessionSeatRequest> SessionSeats { get; set; } = null!;
    }
}
