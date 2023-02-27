namespace Presentation.Requests;
internal record UpdateStationRequest(
    int Id,
    string Name,
    string? Address,
    string? ZipCode,
    string? City,
    string? Latitude,
    string? Longitude,
    TimeSpan? OpenTime,
    TimeSpan? CloseTime);
