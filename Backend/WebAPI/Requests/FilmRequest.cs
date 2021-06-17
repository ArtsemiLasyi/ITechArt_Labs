namespace WebAPI.Requests
{
    public class FilmRequest
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public int DurationInMinutes { get; set; }
        public string? PosterFileName { get; set; }
    }
}
