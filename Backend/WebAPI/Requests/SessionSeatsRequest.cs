using System.Collections.Generic;

namespace WebAPI.Requests
{
    public class SessionSeatsRequest
    {
        public IReadOnlyCollection<SessionSeatRequest> Value { get; set; } = null!;
    }
}
