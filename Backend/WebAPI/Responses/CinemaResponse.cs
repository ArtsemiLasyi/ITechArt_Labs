using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WebAPI.Responses
{
    public class CinemaResponse
    {
        public int Id { get; set; }
        public string CityName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IReadOnlyCollection<HallResponse> Halls { get; set; } = new List<HallResponse>();
    }
}