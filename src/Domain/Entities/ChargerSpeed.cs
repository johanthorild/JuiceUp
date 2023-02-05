using Domain.Entities.Abstractions;

namespace Domain.Entities;

public partial class ChargerSpeed : IEntity
{
    public int Id { get; set; }

    public int Kilowatt { get; set; }

    /// <summary>
    /// Navigational properties />
    /// </summary>

    public virtual ICollection<Charger> Chargers { get; } = new List<Charger>();
}
