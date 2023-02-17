using MediatR;

namespace Application.Commands;

public sealed record DeleteStationCommand(int Id) : IRequest<int>;
