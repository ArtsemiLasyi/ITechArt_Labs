using System.Collections.Generic;

namespace BusinessLogic.Models
{
    public class SessionSeatsModel
    {
        public IReadOnlyCollection<SessionSeatModel> Value { get; set; } = new List<SessionSeatModel>();

        public SessionSeatsModel(IReadOnlyCollection<SessionSeatModel> value)
        {
            Value = value;
        }
    }
}