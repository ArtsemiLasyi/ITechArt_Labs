using System;

namespace WebAPI.Responses
{
    public class SessionResponse
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int HallId { get; set; }
        public DateTime StartDateTime { get; set; }
        public string FilmName { get; set; } = null!;
        public string HallName { get; set; } = null!;
    }
}
