using MediatR;

namespace Application.Commands;

public sealed record CreateStationCommand(
    string Name,
    string? Address,
    string? ZipCode,
    string? City,
    string? Latitude,
    string? Longitude,
    TimeSpan? OpenTime,
    TimeSpan? CloseTime,
    bool? HasRestaurant,
    bool? HasConference,
    bool? HasPersonel,
    bool? HasRestroom) : IRequest<int>;
