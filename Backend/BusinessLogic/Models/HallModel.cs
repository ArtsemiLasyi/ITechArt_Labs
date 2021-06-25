using System.Collections.Generic;

namespace BusinessLogic.Models
{
    public class HallModel
    {
        public int Id { get; set; }
        public int CinemaId { get; set; }
        public string Name { get; set; } = string.Empty;
        public IReadOnlyCollection<SeatModel> Seats { get; set; } = new List<SeatModel>();
    }
}
