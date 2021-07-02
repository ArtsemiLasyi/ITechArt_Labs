namespace WebAPI.Requests
{
    public class SeatTypePriceRequest
    {
        public int SessionId { get; set; }
        public int SeatTypeId { get; set; }
        public PriceRequest Price { get; set; } = null!;
    }
}
