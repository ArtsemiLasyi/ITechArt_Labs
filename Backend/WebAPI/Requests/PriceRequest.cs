namespace WebAPI.Requests
{
    public class PriceRequest
    {
        public decimal Value { get; set; }
        public string Currency { get; set; } = null!;
    }
}
