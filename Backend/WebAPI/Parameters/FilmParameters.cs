﻿using System;

namespace WebAPI.Parameters
{
    public class FilmParameters
    {
        public string? Name { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
    }
}