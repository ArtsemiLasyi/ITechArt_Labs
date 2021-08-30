﻿using System;
using System.Collections.Generic;

namespace WebAPI.Requests
{
    public class OrderRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SessionId { get; set; }
        public DateTime DateTime { get; set; }
        public SeatsRequest Seats { get; set; } = null!;
        public IReadOnlyCollection<ServiceRequest> Services { get; set; } = null!; 
    }
}