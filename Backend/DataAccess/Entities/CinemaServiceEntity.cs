using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class CinemaServiceEntity
    {
        public int CinemaId { get; set; }
        public int ServiceId { get; set; }
        public int CurrencyId { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("CinemaId")]
        public CinemaEntity Cinema { get; set; } = null!;
        [ForeignKey("ServiceId")]
        public ServiceEntity Service { get; set; } = null!;
        [ForeignKey("CurrencyId")]
        public CurrencyEntity Currency { get; set; } = null!;
    }
}
