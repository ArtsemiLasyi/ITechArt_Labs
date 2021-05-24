using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Entities
{
    public partial class UserRoleEntity
    {
        public UserRoleEntity()
        {
            Users = new HashSet<UserEntity>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserEntity> Users { get; set; }
    }
}
