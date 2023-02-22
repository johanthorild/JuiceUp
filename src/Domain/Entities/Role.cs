using Domain.Entities.Abstractions;

namespace Domain.Entities;

public partial class Role : IEntity, IChangeTracked
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime LastChanged { get; set; }

    public string? LastChangedBy { get; set; }

    /// <summary>
    /// Navigational properties />
    /// </summary>

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}

// As in the database
public enum Roles
{
    Reader = 1,
    Admin = 2
}