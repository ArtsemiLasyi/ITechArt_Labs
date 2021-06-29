namespace BusinessLogic.Models
{
    public class PriceModel
    {
        public decimal Value { get; init; }
        public string Currency { get; init; }

        public PriceModel(decimal value, string currency)
        {
            Value = value;
            Currency = currency;
        }
    }
}
