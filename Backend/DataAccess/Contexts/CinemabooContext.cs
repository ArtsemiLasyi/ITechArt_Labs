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
        public virtual DbSet<PosterEntity> Posters { get; set; } = null!;
        public virtual DbSet<CityEntity> Cities { get; set; } = null!;
        public virtual DbSet<HallEntity> Halls { get; set; } = null!;
        public virtual DbSet<CinemaEntity> Cinemas { get; set; } = null!;
        public virtual DbSet<HallPhotoEntity> HallPhotos { get; set; } = null!;
        public virtual DbSet<CinemaPhotoEntity> CinemaPhotos { get; set; } = null!;

        public CinemabooContext(DbContextOptions<CinemabooContext> options)
            : base(options)
        {
        }
    }
}
