using System;

namespace WebAPI.Responses
{
    public class SessionResponse
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int HallId { get; set; }
        public DateTime StartDateTime { get; set; }
    }
}
