﻿namespace WebAPI.Requests
{
    public class HallRequest
    {
        public int CinemaId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

    }
}
