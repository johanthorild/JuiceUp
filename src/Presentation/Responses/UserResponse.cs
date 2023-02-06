namespace Presentation.Responses;
public sealed record UserResponse(
    string Id,
    string Firstname,
    string Lastname,
    string Email,
    DateTime LastChanged,
    string? LastChangedBy,
    UserRoleResponse[] Roles,
    UserCarResponse[] Cars,
    ReservationResponse[] Reservations
);