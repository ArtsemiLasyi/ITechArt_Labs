using System.Collections.Generic;

namespace WebAPI.Responses
{
    public class SessionSeatsResponse
    {
        public IReadOnlyCollection<SessionSeatResponse> Value { get; set; } = null!;
    }
}
