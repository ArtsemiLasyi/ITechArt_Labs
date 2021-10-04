using System;

namespace BusinessLogic.Parameters
{
    public class FilmModelSearchParameters
    {
        public string? FilmName { get; set; }
        public int? CinemaId { get; set; }
        public DateTime? FirstSessionDateTime { get; set; }
        public DateTime? LastSessionDateTime { get; set; }
        public int? FreeSeatsNumber { get; set; }
    }
}
