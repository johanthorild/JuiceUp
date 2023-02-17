namespace Presentation.Requests;
internal sealed record CreateChargerRequest(
    int StationId,
    int ChargerSpeedId);
