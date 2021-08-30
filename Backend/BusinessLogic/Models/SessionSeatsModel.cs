using System.Collections.Generic;

namespace BusinessLogic.Models
{
    public class SessionSeatsModel
    {
        public IReadOnlyCollection<SessionSeatModel> SessionSeats { get; set; } = new List<SessionSeatModel>();

        public SessionSeatsModel(IReadOnlyCollection<SessionSeatModel> sessionSeats)
        {
            SessionSeats = sessionSeats;
        }
    }
}