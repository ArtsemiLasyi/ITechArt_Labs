using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace WebAPI.Responses
{
    public class FilmResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public int DurationInMinutes { get; set; }
        public Stream Poster { get; set; }
        public string PosterFileName { get; set; }
    }
}
