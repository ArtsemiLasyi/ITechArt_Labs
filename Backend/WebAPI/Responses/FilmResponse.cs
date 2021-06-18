using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace WebAPI.Responses
{
    public class FilmResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
