using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public partial class UserEntity
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }

    }
}
