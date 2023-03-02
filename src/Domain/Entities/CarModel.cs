using Domain.Entities.Abstractions;

namespace Domain.Entities;

public partial class CarModel : IEntity, IChangeTracked
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Capacity { get; set; }

    public double RealRange { get; set; }

    public DateTime LastChanged { get; set; }

    public string? LastChangedBy { get; set; }

    /// <summary>
    /// Navigational properties />
    /// </summary>

    public virtual ICollection<UserCar> UserCars { get; } = new List<UserCar>();

    public CarModel(
    int id,
    string name,
    double capacity,
    double realRange,
    DateTime lastChanged,
    string lastChangedBy)
    {
        Id = id;
        Name = name;
        Capacity = capacity;
        RealRange = realRange;
        LastChanged = lastChanged;
        LastChangedBy = lastChangedBy;
    }

    public CarModel(
    string name,
    double capacity,
    double realRange)
    {
        Name = name;
        Capacity = capacity;
        RealRange = realRange;
    }

    public void SetName(string? name)
    {
        if (name is not null)
        {
            Name = name;
        }
    }

    public void SetCapacity(double? capacity)
    {
        if (capacity is not null)
        {
            Capacity = (double)capacity;
        }
    }

    public void SetRealRange(double? realRange)
    {
        if (realRange is not null)
        {
            RealRange = (double)realRange;
        }
    }
}
