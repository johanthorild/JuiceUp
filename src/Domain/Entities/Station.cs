using Domain.Entities.Abstractions;

namespace Domain.Entities;

public partial class Station : IEntity, IChangeTracked
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public string? ZipCode { get; set; }

    public string? City { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public TimeSpan? OpenTime { get; set; }

    public TimeSpan? CloseTime { get; set; }

    public DateTime LastChanged { get; set; }

    public string? LastChangedBy { get; set; }

    /// <summary>
    /// Navigational properties />
    /// </summary>

    public virtual ICollection<Charger> Chargers { get; } = new List<Charger>();

    public Station(
        int id,
        string name,
        DateTime lastChanged,
        string lastChangedBy)
    {
        Id = id;
        Name = name;
        LastChanged = lastChanged;
        LastChangedBy = lastChangedBy;
    }


    public Station(
        string name,
        string? address,
        string? zipCode,
        string? city,
        string? latitude,
        string? longitude,
        TimeSpan? openTime,
        TimeSpan? closeTime)
    {
        Name = name;
        Address = address;
        ZipCode = zipCode;
        City = city;
        Latitude = latitude;
        Longitude = longitude;
        OpenTime = openTime;
        CloseTime = closeTime;
    }

    public void SetName(string? name)
    {
        if (name is not null)
        {
            Name = name;
        }
    }

    public void SetAddress(string? address)
    {
        if (address is not null)
        {
            Address = address;
        }
    }

    public void SetZipCode(string? zipCode)
    {
        if (zipCode is not null)
        {
            ZipCode = zipCode;
        }
    }

    public void SetCity(string? city)
    {
        if (city is not null)
        {
            City = city;
        }
    }

    public void SetOpenTime(TimeSpan? openTime)
    {
        if (openTime is not null)
        {
            OpenTime = openTime;
        }
    }

    public void SetCloseTime(TimeSpan? closeTime)
    {
        if (closeTime is not null)
        {
            CloseTime = closeTime;
        }
    }
}
