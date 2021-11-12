using System;

namespace WebAPI.Parameters
{
    public class FilmRequestSearchParameters
    {
        public string? FilmName { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
