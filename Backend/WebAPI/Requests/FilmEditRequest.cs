using Microsoft.AspNetCore.Http;

namespace WebAPI.Requests
{
    public class FilmEditRequest
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public int DurationInMinutes { get; set; }
        public IFormFile? Poster { get; set; }
    }
}

