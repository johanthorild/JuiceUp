namespace Domain.Entities;
public sealed partial class User
{
    public User()
    {

    }

    public User(string email, string firstname, string lastname, string password, string salt)
    {
        Email = email;
        Firstname = firstname;
        Lastname = lastname;
        Password = password;
        Salt = salt;
    }

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
}
