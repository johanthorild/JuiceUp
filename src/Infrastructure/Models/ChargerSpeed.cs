using Infrastructure.Models.Abstractions;

namespace Infrastructure.Models;

public partial class ChargerSpeed : DbEntity
{
    public int Id { get; set; }

    public int Kilowatt { get; set; }

    /// <summary>
    /// Navigational properties />
    /// </summary>

    public virtual ICollection<Charger> Chargers { get; } = new List<Charger>();
}
