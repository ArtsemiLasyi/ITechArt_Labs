using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class SeatTypePriceEntity
    {
        public int SeatTypeId { get; set; }
        public int SessionId { get; set; }
        public int CurrencyId { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("SessionId")]
        public SessionEntity Session { get; set; } = null!;
        [ForeignKey("SeatTypeId")]
        public SeatTypeEntity SeatType { get; set; } = null!;
        [ForeignKey("CurrencyId")]
        public CurrencyEntity Currency { get; set; } = null!;
    }
}
