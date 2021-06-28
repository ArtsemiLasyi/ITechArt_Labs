namespace WebAPI.Responses
{
    public class CinemaServiceResponse
    {
        public int CinemaId { get; set; }
        public int ServiceId { get; set; }
        public PriceResponse Price { get; set; } = null!;
    }
}
