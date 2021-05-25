using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public partial class UserPasswordEntity
    {
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
        public int UserId { get; set; }

        public virtual UserEntity User { get; set; }
    }
}
