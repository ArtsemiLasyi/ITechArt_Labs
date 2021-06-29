namespace WebAPI.Responses
{
    public class SeatTypeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SeatTypeResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
