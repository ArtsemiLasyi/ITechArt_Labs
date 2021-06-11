using Microsoft.EntityFrameworkCore;
using DataAccess.Entities;

namespace DataAccess.Contexts
{
    public partial class CinemabooContext : DbContext
    {
        public virtual DbSet<UserEntity> Users { get; set; } = null!;
        public virtual DbSet<UserPasswordEntity> UserPasswords { get; set; } = null!;
        public virtual DbSet<UserRoleEntity> UserRoles { get; set; } = null!;

        public virtual DbSet<FilmEntity> Films { get; set; } = null!;

        public CinemabooContext(DbContextOptions<CinemabooContext> options)
            : base(options)
        {
        }
    }
}
