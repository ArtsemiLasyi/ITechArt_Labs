using System.Collections.Generic;

namespace WebAPI.Requests
{
    public class HallRequest
    {
        public int CinemaId { get; set; }
        public string Name { get; set; } = null!;
    }
}
