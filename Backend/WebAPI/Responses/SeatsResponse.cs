using System.Collections.Generic;

namespace WebAPI.Responses
{
    public class SeatsResponse
    {
        public IReadOnlyCollection<SeatResponse> Seats { get; set; } = null!;
    }
}
