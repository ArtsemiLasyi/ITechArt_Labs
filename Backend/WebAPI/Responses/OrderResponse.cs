using System;

namespace WebAPI.Responses
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SessionId { get; set; }
        public PriceResponse Price { get; set; } = null!;
        public DateTime SessionStart { get; set; }
        public int CinemaId { get; set; }
        public string CinemaName { get; set; } = null!;
        public int HallId { get; set; }
        public string HallName { get; set; } = null!;
    }
}
