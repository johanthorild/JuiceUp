using Infrastructure.Models.Abstractions;

namespace Infrastructure.Models;

public partial class CarModel : DbEntity, IChangeTracked
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Capacity { get; set; }

    public DateTime LastChanged { get; set; }

    public string LastChangedBy { get; set; } = null!;

    /// <summary>
    /// Navigational properties />
    /// </summary>

    public virtual ICollection<UserCar> UserCars { get; } = new List<UserCar>();
}
