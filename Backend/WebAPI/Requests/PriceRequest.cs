namespace WebAPI.Requests
{
    public class PriceRequest
    {
        public decimal Value { get; set; }
        public CurrencyRequest Currency { get; set; } = null!;
    }
}
