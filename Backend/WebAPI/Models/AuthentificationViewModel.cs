using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class AuthentificationViewModel
    {
        [EmailAddress(ErrorMessage = "Invalid address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password mustn't be empty")]
        public string Password { get; set; }
    }
}
