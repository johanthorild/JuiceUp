using Infrastructure.Models.Abstractions;

namespace Infrastructure.Models;

public partial class Charger : DbEntity
{
    public int Id { get; set; }

    public int StationId { get; set; }

    public int ChargerSpeedId { get; set; }

    /// <summary>
    /// Navigational properties />
    /// </summary>

    public virtual ChargerSpeed ChargerSpeed { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; } = new List<Reservation>();

    public virtual Station Station { get; set; } = null!;
}
