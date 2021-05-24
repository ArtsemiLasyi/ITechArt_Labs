using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
    }
}
