namespace WebAPI.Responses
{
    public class SeatTypePriceResponse
    {
        public string SeatTypeName { get; set; } = null!;
        public int SessionId { get; set; }
        public int SeatTypeId { get; set; }
        public PriceResponse Price { get; set; } = null!;
    }
}
