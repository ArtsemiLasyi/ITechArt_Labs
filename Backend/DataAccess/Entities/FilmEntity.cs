using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class FilmEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public long DurationInTicks { get; set; }
        public bool IsDeleted { get; set; }
    }
}
