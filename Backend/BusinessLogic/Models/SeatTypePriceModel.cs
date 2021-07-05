namespace BusinessLogic.Models
{
    public class SeatTypePriceModel
    {
        public int SeatTypeId { get; set; }
        public int SessionId { get; set; }
        public PriceModel Price { get; set; } = null!;
    }
}
