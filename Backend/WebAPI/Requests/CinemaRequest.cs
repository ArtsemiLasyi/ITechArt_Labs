using System.Collections.Generic;

namespace WebAPI.Requests
{
    public class CinemaRequest
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CityName { get; set; } = null!;
        public ICollection<ServiceCinemaRequest> ServiceCinemas { get; set; } = null!;
    }
}
