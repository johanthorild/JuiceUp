using Domain.Entities.Abstractions;

namespace Domain.Entities;

public partial class User : IEntity, IChangeTracked
{
    public Guid Id { get; private set; }

    public string Email { get; private set; } = null!;

    public string Firstname { get; private set; } = null!;

    public string Lastname { get; private set; } = null!;

    public string Password { get; private set; } = null!;

    public string Salt { get; private set; } = null!;

    public byte FailedLogins { get; private set; }

    public DateTime? LockedUntil { get; private set; }

    public bool ChangePassword { get; private set; }

    public DateTime LastChanged { get; set; }

    public string? LastChangedBy { get; set; }

    /// <summary>
    /// Navigational properties />
    /// </summary>

    public virtual ICollection<Reservation> Reservations { get; } = new List<Reservation>();

    public virtual ICollection<UserCar> UserCars { get; } = new List<UserCar>();

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();

    public User()
    {
    }

    public User(
        string email,
        string firstname,
        string lastname,
        string password,
        string salt)
    {
        Id = Guid.NewGuid();
        Email = email;
        Firstname = firstname;
        Lastname = lastname;
        Password = password;
        Salt = salt;
        UserRoles = ApplyDefaultRole();
    }

    public void UpdateNames(
        string firstname,
        string lastname)
    {
        Firstname = firstname;
        Lastname = lastname;
    }

    public void UpdateEmail(
    string email)
    {
        Email = email;
    }

    public void UpdatePassword(
    string password,
    string salt)
    {
        Password = password;
        Salt = salt;
    }

    static ICollection<UserRole> ApplyDefaultRole()
    {
        return new List<UserRole>()
        {
           new() { RoleId = 1 }
        };
    }
}
