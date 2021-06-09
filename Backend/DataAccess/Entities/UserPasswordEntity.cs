using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public partial class UserPasswordEntity
    {
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] Salt { get; set; } = null!;
        public int UserId { get; set; }
    }
}
