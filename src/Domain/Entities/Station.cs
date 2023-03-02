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

    public bool? HasRestaurant { get; set; }
    public bool? HasConference { get; set; }
    public bool? HasPersonel { get; set; }
    public bool? HasRestroom { get; set; }

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
        TimeSpan? closeTime,
        bool? hasRestaurant,
        bool? hasConference,
        bool? hasPersonel,
        bool? hasRestroom)
    {
        Name = name;
        Address = address;
        ZipCode = zipCode;
        City = city;
        Latitude = latitude;
        Longitude = longitude;
        OpenTime = openTime;
        CloseTime = closeTime;
        HasRestaurant = hasRestaurant;
        HasConference = hasConference;
        HasPersonel = hasPersonel;
        HasRestroom = hasRestroom;
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

    public void SetLongitude(string? longitude)
    {
        if (longitude is not null)
        {
            Longitude = longitude;
        }
    }

    public void SetLatitude(string? latitude)
    {
        if (latitude is not null)
        {
            Latitude = latitude;
        }
    }

    public void SetHasRestaurant(bool? hasRestaurant)
    {
        if (hasRestaurant is not null)
        {
            HasRestaurant = hasRestaurant;
        }
    }

    public void SetHasConference(bool? hasConference)
    {
        if (hasConference is not null)
        {
            HasConference = hasConference;
        }
    }

    public void SetHasPersonel(bool? hasPersonel)
    {
        if (hasPersonel is not null)
        {
            HasPersonel = hasPersonel;
        }
    }

    public void SetHasRestroom(bool? hasRestroom)
    {
        if (hasRestroom is not null)
        {
            HasRestroom = hasRestroom;
        }
    }
}
