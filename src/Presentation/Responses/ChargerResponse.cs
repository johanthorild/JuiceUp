namespace Presentation.Responses;
public sealed record ChargerResponse(
    int Id,
    int StationId,
    int ChargerSpeedId,
    string ChargerSpeedKilowatt,
    StationResponse? Station
);