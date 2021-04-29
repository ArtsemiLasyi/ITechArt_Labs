using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.DAL.Contexts
{
    public class CinemabooContext : DbContext
    {
        public CinemabooContext (DbContextOptions<CinemabooContext> options)
            : base(options)
        {
        }

        public DbSet<WebAPI.DAL.Entities.UserEntity> Users { get; set; }

        public DbSet<WebAPI.DAL.Entities.UserRoleEntity> UserRoles { get; set; }
    }
}
