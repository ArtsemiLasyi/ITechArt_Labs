﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DataAccess.Entities;

namespace DataAccess.Contexts
{
    public partial class CinemabooContext : DbContext
    {
        public virtual DbSet<UserEntity> Users { get; set; } = null!;
        public virtual DbSet<UserPasswordEntity> UserPasswords { get; set; } = null!;
        public virtual DbSet<UserRoleEntity> UserRoles { get; set; } = null!;

        public CinemabooContext(DbContextOptions<CinemabooContext> options)
            : base(options)
        {
        }
    }
}