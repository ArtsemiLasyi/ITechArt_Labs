namespace BusinessLogic.Models
{
    public class PriceModel
    {
        public decimal Value { get; init; }
        public CurrencyModel Currency { get; init; }

        public PriceModel(decimal value, CurrencyModel currency)
        {
            Value = value;
            Currency = currency;
        }
    }
}
