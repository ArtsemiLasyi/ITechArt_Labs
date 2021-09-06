﻿using System;
using System.Collections.Generic;

namespace BusinessLogic.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SessionId { get; set; }
        public PriceModel Price { get; set; } = null!;
        public DateTime RegistratedAt { get; set; }
        public SessionSeatsModel SessionSeats { get; set; } = null!;
        public IReadOnlyCollection<CinemaServiceModel> CinemaServices { get; set; } = null!;
    }
}
