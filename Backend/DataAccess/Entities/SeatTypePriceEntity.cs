using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class SeatTypePriceEntity
    {
        public int SeatTypeId { get; set; }
        public int SessionId { get; set; }
        public int CurrencyId { get; set; }
        public decimal Price { get; set; }
        public CurrencyEntity Currency { get; set; } = null!;
        public SeatTypeEntity SeatType { get; set; } = null!;
    }
}
