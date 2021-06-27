namespace WebAPI.Requests
{
    public class ServiceCinemaRequest
    {
        public int CinemaId { get; set; }
        public int ServiceId { get; set; }
        public decimal PriceInDollars { get; set; }
    }
}
