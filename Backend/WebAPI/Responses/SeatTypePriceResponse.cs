namespace WebAPI.Responses
{
    public class SeatTypePriceResponse
    {
        public int SessionId { get; set; }
        public int SeatTypeId { get; set; }
        public PriceResponse Price { get; set; } = null!;
    }
}
