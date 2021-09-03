namespace WebAPI.Responses
{
    public class PriceResponse
    {
        public decimal Value { get; set; }
        public CurrencyResponse Currency { get; set; } = null!;
    }
}
