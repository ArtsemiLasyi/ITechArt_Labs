using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BusinessLogic.Models
{
    public class CinemaModel
    {
        public int Id { get; set; }
        public string CityName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IReadOnlyCollection<HallModel> Halls { get; set; } = new List<HallModel>();
    }
}
