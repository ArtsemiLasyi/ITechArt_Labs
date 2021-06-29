using System.Collections.Generic;

namespace BusinessLogic.Models
{
    public class SeatsModel
    {
        public IReadOnlyCollection<SeatModel> Seats { get; set; } = new List<SeatModel>();

        public SeatsModel(IReadOnlyCollection<SeatModel> seats)
        {
            Seats = seats;
        }
    }
}
