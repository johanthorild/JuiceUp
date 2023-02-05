namespace Domain.Entities.Abstractions;

/// <summary>
/// Contains change tracking information.
/// </summary>
public interface IChangeTracked
{
    /// <summary>
    /// Timestamp of last change.
    /// </summary>
    DateTime LastChanged { get; set; }

    /// <summary>
    /// Username of user that performed the last change.
    /// </summary>
    string? LastChangedBy { get; set; }

}
