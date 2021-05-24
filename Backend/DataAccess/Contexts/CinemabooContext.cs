using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DataAccess.Entities;

#nullable disable

namespace DataAccess.Contexts
{
    public partial class CinemabooContext : DbContext
    {
        public CinemabooContext()
        {
        }

        public CinemabooContext(DbContextOptions<CinemabooContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<UserPasswordEntity> UserPasswords { get; set; }
        public virtual DbSet<UserRoleEntity> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasIndex(e => e.Email, "UIX_Users_Email")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(320);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_UserRoles");
            });

            modelBuilder.Entity<UserPasswordEntity>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_UserPasswords_1");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsFixedLength(true);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserPassword)
                    .HasForeignKey<UserPasswordEntity>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPasswords_Users");
            });

            modelBuilder.Entity<UserRoleEntity>(entity =>
            {
                entity.HasIndex(e => e.Name, "UIX_UserRoles_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
