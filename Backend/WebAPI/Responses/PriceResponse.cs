namespace WebAPI.Responses
{
    public class PriceResponse
    {
        public decimal Value { get; set; }
        public string Currency { get; set; } = null!;
    }
}