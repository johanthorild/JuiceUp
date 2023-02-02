using Infrastructure.Models.Abstractions;

namespace Infrastructure.Models;

public partial class UserRole : DbEntity, IChangeTracked
{
    public Guid UserId { get; set; }

    public int RoleId { get; set; }

    public DateTime LastChanged { get; set; }

    public string LastChangedBy { get; set; } = null!;

    /// <summary>
    /// Navigational properties />
    /// </summary>

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
