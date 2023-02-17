using MediatR;

namespace Application.Commands;

public sealed record CreateChargerCommand(
    int StationId,
    int ChargerSpeedId) : IRequest<int>;
