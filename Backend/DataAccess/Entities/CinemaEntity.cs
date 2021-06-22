using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class CinemaEntity
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public ICollection<HallEntity> Halls { get; set; } = null!;
        public CityEntity City { get; set; } = null!;
    }
}
