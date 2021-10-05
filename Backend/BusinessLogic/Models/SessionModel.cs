using System;

namespace BusinessLogic.Models
{
    public class SessionModel
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int HallId { get; set; }
        public DateTime StartDateTime { get; set; }
        public string FilmName { get; set; } = string.Empty;
        public string HallName { get; set; } = string.Empty;
    }
}
