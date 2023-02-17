using Domain.Entities.Abstractions;

namespace Domain.Entities;

public partial class Charger : IEntity
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

    public Charger(
        int stationId,
        int chargerSpeedId)
    {
        StationId = stationId;
        ChargerSpeedId = chargerSpeedId;
    }
}
