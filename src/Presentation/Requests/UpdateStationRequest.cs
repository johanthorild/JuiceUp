namespace Presentation.Requests;
internal record UpdateStationRequest(
    string Name,
    string? Address,
    string? ZipCode,
    string? City,
    string? Latitude,
    string? Longitude,
    TimeSpan? OpenTime,
    TimeSpan? CloseTime);
