using System;

namespace DataAccess.Parameters
{
    public class FilmEntitySearchParameters
    {
        public string? FilmName { get; set; }
        public int? CinemaId { get; set; }
        public DateTime? FirstSessionDateTime { get; set; }
        public DateTime? LastSessionDateTime { get; set; }
    }
}
