using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class HallEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CinemaId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
