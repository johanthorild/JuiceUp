namespace Presentation.Responses;
public sealed record StationResponse(
    int Id,
    string Name,
    string? Address,
    string? ZipCode,
    string? City,
    string? Latitude,
    string? Longitude,
    TimeSpan? OpenTime,
    TimeSpan? CloseTime,
    DateTime? LastChanged,
    string? LastChangedBy,
    ChargerResponse[]? Chargers
);