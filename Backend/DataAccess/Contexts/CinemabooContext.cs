using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class CinemabooContext : DbContext
    {
        public CinemabooContext (DbContextOptions<CinemabooContext> options)
            : base(options)
        {
        }

        public DbSet<DataAccess.Entities.UserEntity> Users { get; set; }

        public DbSet<DataAccess.Entities.UserRoleEntity> UserRoles { get; set; }
    }
}
