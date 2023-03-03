namespace Presentation.Requests;
internal sealed record CreateStationRequest(
    string Name,
    string? Address,
    string? ZipCode,
    string? City,
    string? Latitude,
    string? Longitude,
    bool? HasRestaurant,
    bool? HasConference,
    bool? HasPersonel,
    bool? HasRestroom,
    TimeSpan? OpenTime,
    TimeSpan? CloseTime);
