using System.Collections.Generic;

namespace WebAPI.Responses
{
    public class HallResponse
    {
        public int Id { get; set; }
        public int CinemaId { get; set; }
        public string Name { get; set; } = null!;
        public IReadOnlyCollection<SeatResponse> Seats { get; set; } = new List<SeatResponse>();
    }
}
