﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [EmailAddress(ErrorMessage = "Invalid address")]
        public string Email { get; set; }
    }
}