﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Requests
{
    public class UserRequest
    {
        [MinLength(6, ErrorMessage = "Password must not be less than 6 symbols")]
        public string? Password { get; set; }
    }
}
