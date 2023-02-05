using Domain.Entities.Abstractions;

namespace Domain.Entities;

public partial class UserRole : IEntity
{
    public Guid UserId { get; set; }

    public int RoleId { get; set; }

    /// <summary>
    /// Navigational properties />
    /// </summary>

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
