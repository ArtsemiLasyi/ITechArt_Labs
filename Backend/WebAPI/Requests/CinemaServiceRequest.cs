namespace WebAPI.Requests
{
    public class CinemaServiceRequest
    {
        public int CinemaId { get; set; }
        public int ServiceId { get; set; }
        public PriceRequest Price { get; set; } = null!;
    }
}
