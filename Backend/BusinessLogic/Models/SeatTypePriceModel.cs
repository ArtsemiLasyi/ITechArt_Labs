namespace BusinessLogic.Models
{
    public class SeatTypePriceModel
    {
        public string SeatTypeName { get; set; } = null!;
        public int SeatTypeId { get; set; }
        public int SessionId { get; set; }
        public PriceModel Price { get; set; } = null!;
    }
}
