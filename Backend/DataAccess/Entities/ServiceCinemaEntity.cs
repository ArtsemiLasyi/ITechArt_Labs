using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class ServiceCinemaEntity
    {
        [Key]
        public int ServiceId { get; set; }
        [Key]
        public int CinemasId { get; set; }
        public decimal PriceInDollars { get; set; }
        [ForeignKey("CinemaId")]
        public CinemaEntity Cinema { get; set; } = null!;
        [ForeignKey("ServiceId")]
        public ServiceEntity Service { get; set; } = null!;
    }
}
