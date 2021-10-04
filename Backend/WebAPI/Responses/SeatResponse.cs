namespace WebAPI.Responses
{
    public class SeatResponse
    {
        public int Id { get; set; }
        public int Place { get; set; }
        public int Row { get; set; }
        public int SeatTypeId { get; set; }
        public int HallId { get; set; }
        public string ColorRgb { get; set; } = null!;
    }
}
