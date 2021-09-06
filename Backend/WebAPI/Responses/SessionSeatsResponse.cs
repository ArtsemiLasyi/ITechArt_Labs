using System.Collections.Generic;

namespace WebAPI.Responses
{
    public class SessionSeatsResponse
    {
        public IReadOnlyCollection<SessionSeatResponse> SessionSeats { get; set; } = null!;
    }
}
