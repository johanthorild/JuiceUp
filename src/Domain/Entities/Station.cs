﻿using Domain.Entities.Abstractions;

namespace Domain.Entities;

public partial class Station : IEntity, IChangeTracked
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public string? ZipCode { get; set; }

    public string? City { get; set; }

    public TimeSpan? OpenTime { get; set; }

    public TimeSpan? CloseTime { get; set; }

    public DateTime LastChanged { get; set; }

    public string? LastChangedBy { get; set; }

    /// <summary>
    /// Navigational properties />
    /// </summary>

    public virtual ICollection<Charger> Chargers { get; } = new List<Charger>();
}