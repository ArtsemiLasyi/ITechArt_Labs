using Microsoft.AspNetCore.Http;

namespace WebAPI.Requests
{
    public class FilmRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
