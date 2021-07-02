﻿using System;

namespace DataAccess.Entities
{
    public class SessionEntity
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int HallId { get; set; }
        public DateTime StartDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}