using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Entities
{
    public partial class UserEntity
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }

        public virtual UserRoleEntity Role { get; set; }
        public virtual UserPasswordEntity UserPassword { get; set; }
    }
}
