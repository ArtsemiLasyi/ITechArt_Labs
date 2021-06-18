using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class PosterEntity
    {
        [Key]
        public int FilmId { get; set; }
        public string FileName { get; set; } = null!;
    }
}
