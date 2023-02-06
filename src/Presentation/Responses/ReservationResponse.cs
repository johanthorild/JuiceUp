namespace Presentation.Responses;

public sealed record ReservationResponse(
    string Id,
    int ChargerId,
    DateTime StartTime,
    DateTime EndTime
);