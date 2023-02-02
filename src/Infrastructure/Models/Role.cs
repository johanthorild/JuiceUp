using Infrastructure.Models.Abstractions;

namespace Infrastructure.Models;

public partial class Role : DbEntity, IChangeTracked
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime LastChanged { get; set; }

    public string LastChangedBy { get; set; } = null!;

    /// <summary>
    /// Navigational properties />
    /// </summary>

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}
