namespace WebAPI.Requests
{
    public class PriceRequest
    {
        public decimal Value { get; set; }
        public int CurrencyId { get; set; }
    }
}
