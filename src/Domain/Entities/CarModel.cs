using Domain.Entities.Abstractions;

namespace Domain.Entities;

public partial class CarModel : IEntity, IChangeTracked
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Capacity { get; set; }

    public DateTime LastChanged { get; set; }

    public string? LastChangedBy { get; set; }

    /// <summary>
    /// Navigational properties />
    /// </summary>

    public virtual ICollection<UserCar> UserCars { get; } = new List<UserCar>();
}
