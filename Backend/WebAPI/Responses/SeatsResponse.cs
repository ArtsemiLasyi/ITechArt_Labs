using System.Collections.Generic;

namespace WebAPI.Responses
{
    public class SeatsResponse
    {
        public IReadOnlyCollection<SeatResponse> Value { get; set; } = null!;
    }
}
