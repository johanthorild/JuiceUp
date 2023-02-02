using Infrastructure.Models.Abstractions;

namespace Infrastructure.Models;

public partial class User : DbEntity, IChangeTracked
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public byte FailedLogins { get; set; }

    public DateTime? LockedUntil { get; set; }

    public bool ChangePassword { get; set; }

    public DateTime LastChanged { get; set; }

    public string LastChangedBy { get; set; } = null!;

    /// <summary>
    /// Navigational properties />
    /// </summary>

    public virtual ICollection<Reservation> Reservations { get; } = new List<Reservation>();

    public virtual ICollection<UserCar> UserCars { get; } = new List<UserCar>();

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}
