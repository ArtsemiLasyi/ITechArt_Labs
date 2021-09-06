using System;

namespace WebAPI.Responses
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SessionId { get; set; }
        public PriceResponse Price { get; set; } = null!;
        public DateTime DateTime { get; set; }
    }
}
