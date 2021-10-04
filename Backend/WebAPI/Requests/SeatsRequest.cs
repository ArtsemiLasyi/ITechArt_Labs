using System.Collections.Generic;

namespace WebAPI.Requests
{
    public class SeatsRequest
    {
        public IReadOnlyCollection<SeatRequest> Value { get; set; } = null!;
    }
}
