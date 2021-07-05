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
        public virtual DbSet<SeatEntity> Seats { get; set; } = null!;
        public virtual DbSet<SeatTypeEntity> SeatTypes { get; set; } = null!;
        public virtual DbSet<ServiceEntity> Services { get; set; } = null!;
        public virtual DbSet<CinemaServiceEntity> CinemaServices { get; set; } = null!;
        public virtual DbSet<CurrencyEntity> Currencies { get; set; } = null!;
        public virtual DbSet<SessionEntity> Sessions { get; set; } = null!;
        public virtual DbSet<SeatTypePriceEntity> SeatTypePrices { get; set; } = null!;

        public CinemabooContext(DbContextOptions<CinemabooContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CinemaServiceEntity>()
                .HasKey(nameof(CinemaServiceEntity.CinemaId), nameof(CinemaServiceEntity.ServiceId));

            modelBuilder.Entity<SeatTypePriceEntity>()
                .HasKey(nameof(SeatTypePriceEntity.SessionId), nameof(SeatTypePriceEntity.SeatTypeId));

            modelBuilder.Entity<UserPasswordEntity>()
                .HasKey(nameof(UserPasswordEntity.UserId));

            modelBuilder.Entity<UserPasswordEntity>()
                .HasKey(nameof(UserPasswordEntity.UserId));

            modelBuilder.Entity<PosterEntity>()
                .HasKey(nameof(PosterEntity.FilmId));

            modelBuilder.Entity<HallPhotoEntity>()
                .HasKey(nameof(HallPhotoEntity.HallId));

            modelBuilder.Entity<CinemaPhotoEntity>()
                .HasKey(nameof(CinemaPhotoEntity.CinemaId));
        }
    }
}
