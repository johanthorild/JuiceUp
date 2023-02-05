using Application.Providers;

using Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public partial class AppDbContext : DbContext
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public AppDbContext(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public AppDbContext(DbContextOptions<AppDbContext> options, IDateTimeProvider dateTimeProvider)
        : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public virtual DbSet<CarModel> CarModels { get; set; } = null!;

    public virtual DbSet<Charger> Chargers { get; set; } = null!;

    public virtual DbSet<ChargerSpeed> ChargerSpeeds { get; set; } = null!;

    public virtual DbSet<Reservation> Reservations { get; set; } = null!;

    public virtual DbSet<Role> Roles { get; set; } = null!;

    public virtual DbSet<Station> Stations { get; set; } = null!;

    public virtual DbSet<User> Users { get; set; } = null!;

    public virtual DbSet<UserCar> UserCars { get; set; } = null!;

    public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        if (optionsBuilder.IsConfigured) return;
        optionsBuilder.UseSqlServer(); // We are only ending up here if running EF Core PowerTools to generate database diagram
#endif
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarModel>(entity =>
        {
            entity.Property(e => e.LastChangedBy).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Charger>(entity =>
        {
            entity.HasIndex(e => e.ChargerSpeedId, "IX_Chargers_ChargerSpeedId");

            entity.HasIndex(e => e.Id, "IX_Chargers_Id");

            entity.HasIndex(e => e.StationId, "IX_Chargers_StationId");

            entity.HasOne(d => d.ChargerSpeed).WithMany(p => p.Chargers)
                .HasForeignKey(d => d.ChargerSpeedId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Station).WithMany(p => p.Chargers)
                .HasForeignKey(d => d.StationId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ChargerId });

            entity.HasIndex(e => e.ChargerId, "IX_Reservations_ChargerId");

            entity.HasIndex(e => e.Id, "IX_Reservations_Id");

            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Charger).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.ChargerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.LastChangedBy).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Stations_Id");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.LastChangedBy).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.ZipCode).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email, "IX_Users_Email").IsUnique();

            entity.HasIndex(e => e.Id, "IX_Users_Id").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Firstname).HasMaxLength(255);
            entity.Property(e => e.LastChangedBy).HasMaxLength(255);
            entity.Property(e => e.Lastname).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Salt).HasMaxLength(255);
        });

        modelBuilder.Entity<UserCar>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.CarId });

            entity.HasIndex(e => e.CarId, "IX_UserCars_CarId");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.Car).WithMany(p => p.UserCars)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.UserCars)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId });

            entity.HasIndex(e => e.RoleId, "IX_UserRoles_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        SeedDefaultData(modelBuilder);

        RestrictCascadingDeletesOnAllForeignKeys(modelBuilder);

        OnModelCreatingPartial(modelBuilder);
    }

    private void SeedDefaultData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Reader", LastChanged = _dateTimeProvider.Fixed, LastChangedBy = "DataSeed" },
            new Role { Id = 2, Name = "Admin", LastChanged = _dateTimeProvider.Fixed, LastChangedBy = "DataSeed" });

        modelBuilder.Entity<ChargerSpeed>().HasData(
            new ChargerSpeed { Id = 1, Kilowatt = 11 },
            new ChargerSpeed { Id = 2, Kilowatt = 22 },
            new ChargerSpeed { Id = 3, Kilowatt = 50 });

        modelBuilder.Entity<Station>().HasData(
            new Station { Id = 1, Name = "Stockholm", LastChanged = _dateTimeProvider.Fixed, LastChangedBy = "DataSeed" },
            new Station { Id = 2, Name = "Linköping", LastChanged = _dateTimeProvider.Fixed, LastChangedBy = "DataSeed" },
            new Station { Id = 3, Name = "Jönköping", LastChanged = _dateTimeProvider.Fixed, LastChangedBy = "DataSeed" },
            new Station { Id = 4, Name = "Ljungby", LastChanged = _dateTimeProvider.Fixed, LastChangedBy = "DataSeed" },
            new Station { Id = 5, Name = "Halmstad", LastChanged = _dateTimeProvider.Fixed, LastChangedBy = "DataSeed" });

        modelBuilder.Entity<CarModel>().HasData(
            new CarModel { Id = 1, Name = "Tesla Model 3", Capacity = 57.5, LastChanged = _dateTimeProvider.Fixed, LastChangedBy = "DataSeed" },
            new CarModel { Id = 2, Name = "Kia EV6", Capacity = 54, LastChanged = _dateTimeProvider.Fixed, LastChangedBy = "DataSeed" },
            new CarModel { Id = 3, Name = "MG 4", Capacity = 51, LastChanged = _dateTimeProvider.Fixed, LastChangedBy = "DataSeed" },
            new CarModel { Id = 4, Name = "Opel Corsa-e", Capacity = 45, LastChanged = _dateTimeProvider.Fixed, LastChangedBy = "DataSeed" });
    }

    private static void RestrictCascadingDeletesOnAllForeignKeys(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(
            x => x.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
