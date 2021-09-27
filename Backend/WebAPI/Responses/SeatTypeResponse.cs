namespace WebAPI.Responses
{
    public class SeatTypeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ColorRbg { get; set; }
        public SeatTypeResponse(int id, string name, string colorRgb)
        {
            Id = id;
            Name = name;
            ColorRbg = colorRgb;
        }
    }
}
