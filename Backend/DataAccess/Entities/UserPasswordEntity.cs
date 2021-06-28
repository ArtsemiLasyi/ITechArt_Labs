using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public partial class UserPasswordEntity
    {
        public int UserId { get; set; }
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] Salt { get; set; } = null!;
    }
}
