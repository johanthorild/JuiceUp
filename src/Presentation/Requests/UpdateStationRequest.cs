namespace Presentation.Requests;
internal record UpdateStationRequest(
    int Id,
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
