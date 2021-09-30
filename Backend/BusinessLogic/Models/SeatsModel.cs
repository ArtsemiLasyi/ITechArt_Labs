using System.Collections.Generic;

namespace BusinessLogic.Models
{
    public class SeatsModel
    {
        public IReadOnlyCollection<SeatModel> Value { get; set; }

        public SeatsModel()
        {
            Value = new List<SeatModel>();
        }

        public SeatsModel(IReadOnlyCollection<SeatModel> seats)
        {
            Value = seats;
        }
    }
}
