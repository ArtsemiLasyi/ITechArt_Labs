using System;

namespace WebAPI.Parameters
{
    public class FilmRequestSearchParameters
    {
        public string? FilmName { get; set; }
        public int? CinemaId { get; set; }
        public DateTime? FirstSessionDateTime { get; set; }
        public DateTime? LastSessionDateTime { get; set; }
        public int? FreeSeatsNumber { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
