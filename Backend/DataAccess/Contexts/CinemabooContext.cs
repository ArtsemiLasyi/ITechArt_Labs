using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DataAccess.Entities;

#nullable disable

namespace DataAccess.Contexts
{
    public partial class CinemabooContext : DbContext
    {
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<UserPasswordEntity> UserPasswords { get; set; }
        public virtual DbSet<UserRoleEntity> UserRoles { get; set; }

        public CinemabooContext(DbContextOptions<CinemabooContext> options)
            : base(options)
        {
        }
    }
}
