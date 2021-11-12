using System;
using System.Collections.Generic;

namespace WebAPI.Requests
{
    public class SessionRequest
    {
        public int FilmId { get; set; }
        public int HallId { get; set; }
        public DateTime StartDateTime { get; set; }
        public IReadOnlyCollection<SeatTypePriceRequest> SeatTypePrices { get; set; } = null!;
    }
}
