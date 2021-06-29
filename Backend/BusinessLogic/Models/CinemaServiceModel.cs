namespace BusinessLogic.Models
{
    public class CinemaServiceModel
    {
        public int ServiceId { get; set; }
        public int CinemaId { get; set; }
        public PriceModel Price { get; set; } = null!;
    }
}
